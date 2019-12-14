using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using SourceName.Data.Model;

namespace SourceName.Data.Implementation
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly SourceNameContext _context;

        public RepositoryBase(SourceNameContext context) => _context = context;
        
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.AsEnumerable();
        }

        public virtual TEntity Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
            return entity;
        }

        protected List<TEntity> ApplyPagingAndSorting(
            List<TEntity> baseQuery,
            int? limit,
            int? offset,
            string orderBy,
            string orderDirection,
            Dictionary<string, Func<TEntity, object>> orderBySelectors)
        {
            var query = baseQuery;
            if (!string.IsNullOrWhiteSpace(orderBy) && orderBySelectors.ContainsKey(orderBy))
            {
                var orderBySelector = orderBySelectors[orderBy];
                if (orderDirection.Equals("asc", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderBy(orderBySelector).ToList();
                }
                else
                {
                    query = query.OrderByDescending(orderBySelector).ToList();
                }
            }

            if (offset.HasValue)
            {
              query = query.Skip(offset.Value).ToList();
            }
            if (limit.HasValue)
            {
              query = query.Take(limit.Value).ToList();
            }

            return query;
        }
    }
}