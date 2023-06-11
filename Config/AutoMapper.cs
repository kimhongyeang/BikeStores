using AutoMapper;
using DotNetCore.Dtos;
using DotNetCore.Models;

namespace DotNetCore.Config
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Category, GetCategoryDto>().ReverseMap();
            CreateMap<Category, AddCategoryDto>().ReverseMap();
        }
    }
}
