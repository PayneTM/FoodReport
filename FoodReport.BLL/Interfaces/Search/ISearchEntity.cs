using System.Threading.Tasks;

namespace FoodReport.BLL.Interfaces.Search
{
    public interface ISearchEntity<T>
    {
        Task<T> Search(string criteria, string value);
    }
}