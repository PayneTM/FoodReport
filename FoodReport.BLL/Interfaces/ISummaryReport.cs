using System;
using System.Threading.Tasks;

namespace FoodReport.BLL.Interfaces
{
    public interface ISummaryReport<T>
    {
        Task<T> CreateSummary(DateTime fromDate, DateTime toDate);
    }
}