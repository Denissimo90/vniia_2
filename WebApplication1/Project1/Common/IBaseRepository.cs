using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Common
{
    public interface IBaseRepository<T> where T : class
    {
        T GetEntity(int id);
        List<T> GetEntities();
        void Add(T entity);
        void Delete(int entityId);
        void Delete(T entity);
        void Update(T entity);

        void Attach(T entity);
        //void Create(T item);
        //T FindById(int id);
        //IEnumerable<T> Get();
        //IEnumerable<T> Get(Func<T, bool> predicate);
        //void Remove(T item);
        //void Update(T item);
        //void Save();
    }
}
