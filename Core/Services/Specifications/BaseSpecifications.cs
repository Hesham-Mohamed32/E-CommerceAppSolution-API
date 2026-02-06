using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Criteria [Where]
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        #endregion

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        } 
        #endregion

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescendind { get; private set; }

      

        protected void AddOrderBy(Expression<Func<TEntity,object>> orderByExpression) => OrderBy=orderByExpression;
        protected void AddOrderByDescending(Expression<Func<TEntity,object>> orderByDescendingExpression) => OrderByDescendind=orderByDescendingExpression;

        #region Pagination [Skip - Take]
        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; private set; } 
        protected void ApplyPagination(int PageSize , int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize; 
        }
        #endregion

    }
}
