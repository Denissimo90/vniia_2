using Microsoft.EntityFrameworkCore;
using ReportApp.Common;
using ReportApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Common
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T GetEntity(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetEntities()
        {
            return _context.Set<T>().ToList();
        }

        public void Add(T entity)
        {
             _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(int entityId)
        {
            Delete(GetEntity(entityId));
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void Delete(List<T> listEntity)
        {
            throw new NotImplementedException();
        }

        //ApplicationDbContext _context;
        //DbSet<T> _entities;
        //DbSet<T> _dbSet;

        //public BaseRepository(ApplicationDbContext context)
        //{
        //    _context = context;
        //    _dbSet = context.Set<T>();
        //}

        //public IEnumerable<T> Get()
        //{
        //    return _dbSet.AsNoTracking().ToList();
        //}

        //public IEnumerable<T> Get(Func<T, bool> predicate)
        //{
        //    return _dbSet.AsNoTracking().Where(predicate).ToList();
        //}
        //public T FindById(int id)
        //{
        //    return _dbSet.Find(id);
        //}

        //public void Create(T item)
        //{
        //    _dbSet.Add(item);
        //    //_context.SaveChanges();
        //}
        //public void Update(T item)
        //{
        //    _context.Entry(item).State = EntityState.Modified;
        //    //_context.SaveChanges();
        //}
        //public void Remove(int id)
        //{
        //    _dbSet.Remove(_dbSet.Find(id));
        //    //_context.SaveChanges();
        //}
        //public void Remove(T item)
        //{
        //    _dbSet.Remove(item);
        //    //_context.SaveChanges();
        //}

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}
    }
}
