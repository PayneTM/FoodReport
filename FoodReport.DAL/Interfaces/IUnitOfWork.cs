namespace FoodReport.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products();
        IReportRepository Reports();
        IUserRepo Users();
        IRoleRepo Roles();
    }
}