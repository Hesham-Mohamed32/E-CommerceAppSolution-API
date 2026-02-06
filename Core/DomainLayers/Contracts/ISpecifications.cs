using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ISpecifications<TEntity,TKey>  where TEntity:BaseEntity<TKey>
    {
        //signture For Property [Expression ==> Where ]
        public Expression<Func<TEntity,bool>>? Criteria { get; }
        //signture For Property Expression Indclude
        public List<Expression<Func<TEntity,object>>> IncludeExpressions { get; }
        public Expression<Func<TEntity,object>> OrderBy { get; }
        public Expression<Func<TEntity, object>> OrderByDescendind { get; }
        //Skip - Take [ints]
        public int Skip {  get; }
        public int Take { get; }
        public bool IsPaginated { get; }
    }
}
