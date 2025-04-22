using AutoMapper;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Mapping
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterUserDto, User>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
