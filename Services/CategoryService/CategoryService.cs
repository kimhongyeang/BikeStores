using AutoMapper;
using DotNetCore.Dtos;
using DotNetCore.Models;

namespace DotNetCore.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private static List<Category> categories = new List<Category>
        {
            new Category(),
            new Category{Id=1,Name="Water"}
        };

        private readonly IMapper _mapper;
        public CategoryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory)
        {
            var serviceRespone = new ServiceResponse<List<GetCategoryDto>>();
            categories.Add(_mapper.Map<Category>(newCategory));
            serviceRespone.Data = categories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList();
            return serviceRespone;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetCategories()
        {
            var serviceRespone = new ServiceResponse<List<GetCategoryDto>>();
            serviceRespone.Data = categories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList();
            return serviceRespone;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategoryById(long id)
        {
            var serviceRespone = new ServiceResponse<GetCategoryDto>();
            var category = categories.FirstOrDefault(c => c.Id == id);
            serviceRespone.Data = _mapper.Map<GetCategoryDto>(category);
            return serviceRespone;
        }
    }
}
