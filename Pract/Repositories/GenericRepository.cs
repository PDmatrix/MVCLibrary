using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Pract.App_LocalResources;
using Pract.Interfaces;
using Pract.Models;

namespace Pract.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        readonly DbContext _context;
        readonly DbSet<TEntity> _dbSet;

        protected GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
 
        public virtual IEnumerable<TEntity> GetList(int page)
        {
            return _dbSet.AsNoTracking().ToArray();
        }

        public virtual TEntity Find(int? id)
        {
            return _dbSet.Find(id);
        }
 
        public virtual void Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }
        public virtual void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public virtual void Delete(int? id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
            {
                _dbSet.Remove(item);
                _context.SaveChanges();
            }
        }
        public virtual PagingViewModel<TEntity> PagingIndex(IQueryable<TEntity> items, int page)
        {
            IEnumerable<TEntity> itemsPerPages= items.Skip((page - 1) * ResourceClass.PageSize).Take(ResourceClass.PageSize);
            PageInfo pageInfo = new PageInfo { PageNumber=page, TotalItems= items.Count()};
            return new PagingViewModel<TEntity> { PageInfo = pageInfo, Elems = itemsPerPages };
        }
    }
}