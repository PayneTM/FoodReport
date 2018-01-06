using AutoMapper;
using FoodReport.Common.Interfaces;
using FoodReport.DAL.Models;
using FoodReport.Models;
using FoodReport.Models.Account;
using FoodReport.Models.Admin;

namespace FoodReport.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginViewModel, User>();
            CreateMap<EditUserViewModel, User>();
        }
    }
}
