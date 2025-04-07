using BoilerPlate.Domain.Entities.Bl.Paginated;
using BoilerPlate.Domain.Entities.Enums;
using BoilerPlate.Domain.Interfaces.Base;
using BoilerPlate.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BoilerPlate.Infrastructure.Repositories.Base
{
    public class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DataContext Context = context;

        public Dictionary<string, Expression<Func<TEntity, object, bool>>> Predicates { get; set; } = [];
        public List<string> GlobalFilterWhiteList { get; set; } = [];
        public IQueryable<TEntity> GetQuery()
        {
            return Context
                .Set<TEntity>()
                .Where(x => EF.Property<StatusEntityEnum>(x, "StatusEntity") == StatusEntityEnum.Active)
                .AsQueryable();
        }
        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await GetQuery()
                 .ToListAsync();
        }
        public virtual async Task<TEntity?> GetNoStatusByIdAsync(long id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity?> GetByIdAsync(long id)
        {
            return await GetQuery()
                .FirstOrDefaultAsync(x => EF.Property<long>(x, "Id") == id);
        }

        public async Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await GetQuery()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetPagedReponseAsync<TRequest>(TRequest dto) where TRequest : BasePaginatedRequest
        {
            return await GetQuery()
                .Filter(Predicates, GlobalFilterWhiteList, dto)
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();
        }

        public async Task<int> CountEntitiesAsync<TRequest>(TRequest dto) where TRequest : BasePaginatedRequest
        {
            return await GetQuery()
                .Filter(Predicates, GlobalFilterWhiteList, dto)
                .CountAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entity)
        {
            await Context.Set<TEntity>().AddRangeAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entity)
        {
            Context.Set<TEntity>().RemoveRange(entity);
            await Context.SaveChangesAsync();
        }

        public bool IsActive(StatusEntityEnum status)
        {
            if (status == StatusEntityEnum.Active) { return true; } else { return false; }
        }

    }
}