using System.Collections.Generic;

namespace Pract.Interfaces
{
    public interface IRepository<T> where T : class 
    {
        IEnumerable<T>  GetList(int page);
        T Find(int? id);
        void Create(T item);
        void Update(T item);
        void Delete(int? id);
    }
}
