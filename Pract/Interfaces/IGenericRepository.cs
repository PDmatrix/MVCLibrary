using System.Collections.Generic;
using System.Linq;
using Pract.App_LocalResources;
using Pract.Models;

namespace Pract.Interfaces
{
    public interface IGenericRepository<T> where T : class 
    {
        IEnumerable<T>  GetList(int page);
        T Find(int? id);
        void Create(T item);
        void Update(T item);
        void Delete(int? id);
        PagingViewModel<T> PagingIndex(IQueryable<T> items, int page);
    }
}
