using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using Microsoft.Extensions.Options;

namespace FoodReport.DAL.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IOptions<Settings> _settings;
        private IProductRepository _productRepository;
        private IReportRepository _reportRepository;
        private IRoleRepo _roleRepo;
        private IUserRepo _userRepository;

        public UnitOfWork(IOptions<Settings> settings)
        {
            _settings = settings;
        }

        public IProductRepository Products()
        {
            return _productRepository ?? (_productRepository = new ProductRepo(_settings));
        }

        public IReportRepository Reports()
        {
            return _reportRepository ?? (_reportRepository = new ReportRepo(_settings));
        }

        public IUserRepo Users()
        {
            return _userRepository ?? (_userRepository = new UserRepo(_settings));
        }

        public IRoleRepo Roles()
        {
            return _roleRepo ?? (_roleRepo = new RoleRepo(_settings));
        }
    }
}