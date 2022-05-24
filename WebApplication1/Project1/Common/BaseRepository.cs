using App.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ReportApp.Common
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InsertOrUpdate(T entity, int id)
        {
            var _record = GetEntity(id);
            //string entityId = entity.GetType().GetProperty("Id").GetValue(entity, null).ToString();
            //_context.Entry(entity).State = entityId == "0" ? EntityState.Added : EntityState.Modified;
            //_context.SaveChanges();
            if (_record == null)
            {
                Add(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
        public void ForceUpdate(T entity)
        {
            _context.Entry<T>(entity).Reload();
        }
        public T GetEntity(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> GetEntities()
        {
            return _context.Set<T>();
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

            _context.Entry(entity).State = EntityState.Modified;
            //_context.Set<T>().Update(entity);
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

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>()
                    .Where(expression);

        }

        public IQueryable<T> AllIncluding(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Include(expression);
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
