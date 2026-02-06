using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Reposteries
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _Reposteries = [];
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var TypeName = typeof(TEntity).Name;
            if(_Reposteries.ContainsKey(TypeName))
            {
                return (IGenericRepository<TEntity, Tkey>) _Reposteries[TypeName];
            }
            else
            {
                var Repo = new GenericRepository<TEntity, Tkey>(_dbContext);
                 _Reposteries.Add(TypeName, Repo);
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
