using FoodReport.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FoodReport.BLL.Models;
using FoodReport.DAL.Models;
using System.Threading.Tasks;
using System.Linq;
using FoodReport.DAL.Interfaces;

namespace FoodReport.BLL.Services
{
    public class SearchProductService : ISearchProduct
    {
        private readonly IUnitOfWork _unitOfWork;
        public SearchProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SearchModel<Product>> Search(string criteria, string value)
        {
            var product = await _unitOfWork.Products().GetAll();
            try
            {
                return await GetInernalProduct(product, criteria, value);
            }
            catch
            {
                throw new Exception();
            };
        }
        private async Task<SearchModel<Product>> GetInernalProduct(IEnumerable<Product> product, string criteria, string value)
        {
            var model = new SearchModel<Product>();
            switch (criteria)
            {
                case "owner":
                    model.List = product.Where(x => x.Name == value);
                    model.Message = "Your result for name - " + value;
                    break;
                case "status":
                    model.List = product.Where(x => x.Provider == value);
                    model.Message = "Your result for provider - " + value;
                    break;
            }
            return model;
        }
    }
}
