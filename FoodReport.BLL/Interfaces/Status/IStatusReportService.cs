using FoodReport.BLL.Models;
using FoodReport.DAL.Abstractions;
using FoodReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodReport.BLL.Interfaces.Status
{
    public interface IStatusReportService :IStatusService<Field>, IApproveStatus<AdminChangeReportStatus>
    {
    }
}
