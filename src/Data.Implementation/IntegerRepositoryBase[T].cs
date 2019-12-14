using System.Linq;

using SourceName.Data.Model;

namespace SourceName.Data.Implementation
{
    public abstract class IntegerRepositoryBase<TEntity> : RepositoryBase<TEntity>, IIntegerRepository<TEntity>
        where TEntity : EntityWithIntegerId
    {
        public IntegerRepositoryBase(SourceNameContext context) : base(context) {}

        public virtual void Delete(int id)
        {
            var entity = _context.Set<TEntity>().Single(x => x.Id == id);
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual TEntity GetById(int id)
        {
            return _context.Set<TEntity>().SingleOrDefault(x => x.Id == id);
        }
    }
}