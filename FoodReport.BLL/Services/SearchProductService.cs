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
            try
            {
                var product = await _unitOfWork.Products().GetAll();
                return await GetInernalProduct(product, criteria, value);
            }
            catch(Exception ex)
            {
                throw ex;
            };
        }
        private async Task<SearchModel<Product>> GetInernalProduct(IEnumerable<Product> product, string criteria, string value)
        {
            var model = new SearchModel<Product>();
            switch (criteria.ToLower())
            {
                case "provider":
                    model.List = product.Where(x => x.Provider.ToLower().Contains(value.ToLower()));
                    model.Message = "Your result for provider - " + value;
                    break;
                case "name":
                    model.List = product.Where(x => x.Name.ToLower().Contains(value.ToLower()));
                    model.Message = "Your result for name - " + value;
                    break;
                default: throw new Exception(criteria + " - wrong criteria");
            }
            if (model.List.Count() == 0) throw new Exception("Nothing found on - " + value);
            return model;
        }
    }
}
