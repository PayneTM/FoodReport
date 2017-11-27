using FoodReport.DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodReport.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products();
        IReportRepository Reports();
    }
}
