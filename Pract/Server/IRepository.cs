using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pract.Models;

namespace Pract.Server
{
    internal interface IRepository<T> where T : class 
    {
        IEnumerable<T>  GetList(int page);
        T Find(int? id);
        void Create(T item);
        void Update(T item);
        void Delete(int? id);
    }
}
