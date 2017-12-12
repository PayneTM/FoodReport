using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodReport.BLL.Interfaces
{
    public interface ISearchEntity<T>
    {
        Task<T> Search(string criteria, string value);
    }
}
