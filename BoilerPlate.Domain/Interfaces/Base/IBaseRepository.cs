using BoilerPlate.Domain.Entities.Enums;
using System.Linq.Expressions;

namespace BoilerPlate.Domain.Interfaces.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Dictionary of predicates to apply filters
        /// </summary>
        public Dictionary<string, Expression<Func<TEntity, object, bool>>> Predicates { get; set; }

        /// <summary>
        /// WhiteList to apply global filters into predicates
        /// </summary>
        public List<string> GlobalFilterWhiteList { get; set; }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity?> GetByIdAsync(long id);

        /// <summary>
        /// GetByIdAsync (No matter status)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity?> GetNoStatusByIdAsync(long id);

        /// <summary>
        /// GetPagedReponseAsync
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int pageNumber, int pageSize);

        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> InsertAsync(IEnumerable<TEntity> entity);

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(IEnumerable<TEntity> entity);

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<TEntity>> GetAllAsync();

        /// <summary>
        /// Returns if the record status is Active
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        bool IsActive(StatusEntityEnum status);
    }
}
