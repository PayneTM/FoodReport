using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodReport.BLL.Interfaces
{
    public interface IStatusService<T>
    {
        Task Created(List<T> list, string owner);
        Task Edited();
    }
}
