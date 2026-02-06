using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Reposteries
{
    public class GenericRepository<TEntity,TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity:BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity)
        
           => await _dbContext.Set<TEntity>().AddAsync(entity);

        public async Task<IEnumerable<TEntity>>GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();



        public void Remove(TEntity entity)
        
           =>  _dbContext.Set<TEntity>().Remove(entity);
        


        public async Task<TEntity?> GetByIdAsync(TKey id)
      => await _dbContext.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
        
            => _dbContext.Set<TEntity>().Update(entity);

        #region Specifications
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        => await  SpecificationEvulator.CreateQuery(_dbContext.Set<TEntity>(), specifications).ToListAsync();
           
        

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        
           =>  await SpecificationEvulator.CreateQuery(_dbContext.Set<TEntity>() , specifications).FirstOrDefaultAsync();

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)

          => await SpecificationEvulator.CreateQuery(_dbContext.Set<TEntity>(), specifications).CountAsync();
        

        #endregion
    }
}
