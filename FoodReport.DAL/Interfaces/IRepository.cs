using System;
using System.Collections.Generic;
using System.Text;

namespace FoodReport.DAL.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        T Get(int? id);
        IEnumerable<T> GetAll();
        void Create(T item);
        T Update(T item);
        void Delete(int? id);
    }
}
