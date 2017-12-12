using FoodReport.BLL.Models;
using FoodReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodReport.BLL.Interfaces
{
    public interface ISearchService
    {
        ISearchProduct Product();
        ISearchReport Report();
    }
}
