using FoodReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodReport.BLL.Models
{
    public class SearchModel<T>
    {
        public IEnumerable<T> List { get; set; }
        public string Message { get; set; }
    }
}
