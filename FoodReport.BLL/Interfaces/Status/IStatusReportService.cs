using FoodReport.BLL.Models;
using FoodReport.DAL.Models;

namespace FoodReport.BLL.Interfaces.Status
{
    public interface IStatusReportService : IStatusService<Field>, IApproveStatus<AdminChangeReportStatus>
    {
    }
}