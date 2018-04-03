﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Pract.Models;

namespace Pract.Server
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
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

    }
}