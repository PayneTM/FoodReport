using AutoMapper;
using FoodReport.Common.Interfaces;
using FoodReport.Models.Account;

namespace FoodReport.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginViewModel, IUser>();
        }
    }
}