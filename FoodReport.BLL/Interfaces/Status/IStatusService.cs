using FoodReport.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodReport.BLL.Interfaces.Status
{
    public interface IStatusService<T>
    {
        Task onCreateStatus(List<T> list, string owner);
        Task onEditStatus(EditReportModel<T> item, string owner);
    }
}
