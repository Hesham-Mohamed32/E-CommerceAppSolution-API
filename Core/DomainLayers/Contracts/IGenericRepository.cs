using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity:BaseEntity<TKey>
    {
        //Get All
        Task<IEnumerable<TEntity>> GetAllAsync();
        //Get by Id
        Task<TEntity?> GetByIdAsync(TKey id);
        //Add
        Task AddAsync(TEntity entity);
        //Update
        void Update(TEntity entity);
        //Delete
        void Remove(TEntity entity);
        #region Specification
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,TKey> specifications);
        
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
        Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);
        #endregion

    }
}
