using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodReport.DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll(string id = null);
        Task<T> Get(string id);
        Task<bool> Remove(string id);
        Task<bool> Update(string id, T item);
        Task Add(T item);
        //Task<bool> UpdateDocument(string id, string body);
    }
}
