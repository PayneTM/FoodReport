using System.Collections.Generic;
using System.Threading.Tasks;
using FoodReport.BLL.Models;

namespace FoodReport.BLL.Interfaces.Status
{
    public interface IStatusService<T>
    {
        Task onCreateStatus(List<T> list, string owner);
        Task onEditStatus(EditReportModel<T> item, string owner);
    }
}