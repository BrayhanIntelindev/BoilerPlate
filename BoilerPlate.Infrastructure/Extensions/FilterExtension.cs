using System;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BoilerPlate.Domain.Entities.Bl.Paginated;
using BoilerPlate.Domain.Entities.Enums;

namespace BoilerPlate.Infrastructure.Extensions
{
    public static class FilterExtension
    {
        public static IQueryable<TEntity> Filter<TEntity, TFilter>(
            this IQueryable<TEntity> source,
            Dictionary<string, Expression<Func<TEntity, object, bool>>> predicates,
            Dictionary<string, Expression<Func<TEntity, object, bool>>> predicateNotContains,
            List<string> notContainFilter,
            TFilter filterDto)
            where TFilter : BasePaginatedRequest
            where TEntity : class, new()
        {
            source = source.Where(x => EF.Property<StatusEntityEnum>(x, "StatusEntity") == StatusEntityEnum.Active);
            if (!string.IsNullOrEmpty(filterDto.GroupBy) && !string.IsNullOrEmpty(filterDto.GroupView))
            {
                source = ApplyDynamicFilterQuery(source, filterDto.GroupBy, filterDto.GroupView);
            }
            source = ApplyFilters(source, predicates, predicateNotContains, notContainFilter, filterDto);
            source = OrderQuery(source, filterDto);
            return source;

            // select *
            // from x
            // where (x.GroupTerm == KeyView )
            // and (x.propertyA == valueA1 or x.propertyA == valueA2 or x.propertyA == valueA3)
            // and (x.propertyB != valueB1 or x.propertyB != valueB2)
            // and x.propertyC == valueC
            // and x.propertyD != valueD
            // order by x.OrderBy OrderDirection 

        }

        private static IQueryable<TEntity> GlobalFilter<TEntity>(
            IQueryable<TEntity> source,
            Dictionary<string, Expression<Func<TEntity, object, bool>>> predicates,
            List<string> globalFilterWhiteList,
            string filterDto)
            where TEntity : class, new()
        {
            if (!string.IsNullOrWhiteSpace(filterDto))
            {
                source = BuildGlobalOrExpression(source, predicates, globalFilterWhiteList, filterDto);
            }

            return source;
        }

        public static IQueryable<TEntity> ApplyFilters<TEntity, TFilter>(
            IQueryable<TEntity> source,
            Dictionary<string, Expression<Func<TEntity, object, bool>>> predicates,
            Dictionary<string, Expression<Func<TEntity, object, bool>>> predicateNotContains,
            List<string> notContainFilter,
            TFilter filterDto)
            where TEntity : class, new()
            where TFilter : BasePaginatedRequest
        {
            foreach (var key in predicates.Keys.Union(predicateNotContains.Keys))
            {
                var property = typeof(TFilter).GetProperty(key);
                if (property != null)
                {
                    var value = property.GetValue(filterDto);
                    if (value != null)
                    {
                        var values = GetValuesFromProperty(value, property.PropertyType);
                        if (values.Count == 0)
                        {
                            continue;
                        }

                        var predicate = notContainFilter.Contains(key) ? predicateNotContains[key] : predicates[key];

                        if (values.Count == 1)
                        {
                            source = BuildExpression(source, new KeyValuePair<string, Expression<Func<TEntity, object, bool>>>(key, predicate), values[0]);
                        }
                        else
                        {
                            source = BuildOrExpression(source, new KeyValuePair<string, Expression<Func<TEntity, object, bool>>>(key, predicate), values);
                        }
                    }
                }
            }

            return source;
        }

        static List<object> GetValuesFromProperty(object value, Type propertyType)
        {
            var values = new List<object>();

            if (propertyType == typeof(string))
            {
                values = value?
                    .ToString()?
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(v => (object)v.Trim()).ToList() ?? new List<object>();
            }
            else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(List<>))
            {
                values = (List<object>)(value as IEnumerable<object> ?? new List<object>());
            }
            else if (propertyType.IsEnum || (propertyType.IsValueType && propertyType != typeof(bool)))
            {
                if (value != null && !value.Equals(Activator.CreateInstance(propertyType)))
                {
                    values.Add(value);
                }
            }
            else
            {
                if (value != null)
                {
                    values.Add(value);
                }
            }

            return values;
        }

        static IQueryable<TEntity> BuildExpression<TEntity>(
            IQueryable<TEntity> source,
            KeyValuePair<string, Expression<Func<TEntity, object, bool>>> predicate,
            object val)
            where TEntity : class, new()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var body = Expression.Invoke(predicate.Value, parameter, Expression.Constant(val));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(body, parameter);
            return source.Where(lambda);
        }

        static IQueryable<TEntity> BuildOrExpression<TEntity>(
            IQueryable<TEntity> source,
            KeyValuePair<string, Expression<Func<TEntity, object, bool>>> predicate,
            List<object> values)
            where TEntity : class, new()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            Expression? orExpression = null;

            foreach (var val in values)
            {
                var body = Expression.Invoke(predicate.Value, parameter, Expression.Constant(val));

                if (orExpression == null)
                {
                    orExpression = body;
                }
                else
                {
                    orExpression = Expression.OrElse(orExpression, body);
                }
            }

            if (orExpression != null)
            {
                var lambda = Expression.Lambda<Func<TEntity, bool>>(orExpression, parameter);
                source = source.Where(lambda);
            }

            return source;
        }

        static IQueryable<TEntity> BuildGlobalOrExpression<TEntity>(
            IQueryable<TEntity> source,
            Dictionary<string, Expression<Func<TEntity, object, bool>>> predicates,
            List<string> globalFilterWhiteList,
            string val)
            where TEntity : class, new()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            Expression? orExpression = null;

            foreach (var predicate in predicates)
            {
                if (!globalFilterWhiteList.Contains(predicate.Key))
                {
                    continue;
                }
                var body = Expression.Invoke(predicate.Value, parameter, Expression.Constant(val));

                if (orExpression == null)
                {
                    orExpression = body;
                }
                else
                {
                    orExpression = Expression.OrElse(orExpression, body);
                }
            }

            if (orExpression != null)
            {
                var lambda = Expression.Lambda<Func<TEntity, bool>>(orExpression, parameter);
                source = source.Where(lambda);
            }

            return source;
        }

        public static IQueryable<TEntity> OrderQuery<TEntity, TFilter>(IQueryable<TEntity> source, TFilter filterDto)
            where TFilter : BasePaginatedRequest
            where TEntity : class, new()
        {
            if (string.IsNullOrWhiteSpace(filterDto.SortBy))
            {
                return source;
            }

            var orderMethod = filterDto.SortOrder == "desc" ? $"{filterDto.SortBy} desc" : filterDto.SortBy;
            return source.OrderBy(orderMethod);
        }

        public static IQueryable<TEntity> ApplyDynamicFilterQuery<TEntity>(
            this IQueryable<TEntity> source,
            string propertyPath,
            string value)
            where TEntity : class, new()
        {
            if (string.IsNullOrWhiteSpace(propertyPath))
            {
                throw new ArgumentException("La ruta de la propiedad no puede estar vacía.", nameof(propertyPath));
            }

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            Expression property = parameter;

            foreach (var member in propertyPath.Split('.'))
            {
                property = Expression.Property(property, member);
            }

            var lambda = Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(property, Expression.Constant(value)),
                parameter
            );

            return source.Where(lambda);
        }
    }
}
