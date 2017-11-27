using FoodReport.DAL.Data;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodReport.DAL.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProductRepository _productRepository;
        private IReportRepository _reportRepository;
        private readonly IOptions<Settings> _settings;
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
    }
}
