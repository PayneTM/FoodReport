using FoodReport.BLL.Models;
using FoodReport.DAL.Models;

namespace FoodReport.BLL.Interfaces.Search
{
    public interface ISearchProduct : ISearchEntity<SearchModel<Product>>
    {
    }
}