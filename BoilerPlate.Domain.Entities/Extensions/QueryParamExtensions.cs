using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BoilerPlate.Domain.Entities.Extensions
{
    public static class QueryParamExtensions
    {
        public static string ToQueryString<T>(this T obj)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                      .Where(p => p.GetValue(obj, null) != null)
                                      .Select(p => $"{HttpUtility.UrlEncode(p.Name)}={HttpUtility.UrlEncode(p.GetValue(obj, null).ToString())}");

            return string.Join("&", properties);
        }
    }
}
