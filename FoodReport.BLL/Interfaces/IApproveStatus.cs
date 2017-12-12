using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodReport.BLL.Interfaces
{
    public interface IApproveStatus<T>
    {
        Task onApproveStatus(T item, string owner);
    }
}
