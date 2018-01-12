using FoodReport.BLL.Interfaces.Search;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using FoodReport.DAL.Repos;
using Microsoft.Extensions.Options;

namespace FoodReport.BLL.Services
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ISearchProduct _searchProductService;
        private ISearchReport _searchReportService;

        public SearchService(IOptions<Settings> options)
        {
            _unitOfWork = new UnitOfWork(options);
        }

        public ISearchProduct Product()
        {
            return _searchProductService ?? (_searchProductService = new SearchProductService(_unitOfWork));
        }

        public ISearchReport Report()
        {
            return _searchReportService ?? (_searchReportService = new SearchReportService(_unitOfWork));
        }
    }
}