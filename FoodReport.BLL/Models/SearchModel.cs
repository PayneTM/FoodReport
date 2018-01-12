using System.Collections.Generic;

namespace FoodReport.BLL.Models
{
    public class SearchModel<T>
    {
        public IEnumerable<T> List { get; set; }
        public string Message { get; set; }
    }
}