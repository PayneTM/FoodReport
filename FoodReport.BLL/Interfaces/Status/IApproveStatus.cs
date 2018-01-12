using System.Threading.Tasks;

namespace FoodReport.BLL.Interfaces.Status
{
    public interface IApproveStatus<T>
    {
        Task onApproveStatus(T item, string owner);
    }
}