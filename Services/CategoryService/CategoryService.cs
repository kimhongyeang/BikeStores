using AutoMapper;
using DotNetCore.Data;
using DotNetCore.Dtos;
using DotNetCore.Models;
using Microsoft.EntityFrameworkCore;

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
        private readonly DataContext _context;

        public CategoryService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory)
        {
            var serviceRespone = new ServiceResponse<List<GetCategoryDto>>();
            var category = _mapper.Map<Category>(newCategory);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            serviceRespone.Data = await _context.Categories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToListAsync();
            return serviceRespone;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> DeleteCategory(long id)
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    throw new Exception($"Category cannot find ID : {id}");
                }
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Categories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToListAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetCategories()
        {
            var serviceRespone = new ServiceResponse<List<GetCategoryDto>>();
            var dbCategories = await _context.Categories.ToListAsync();
            serviceRespone.Data = dbCategories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList();
            return serviceRespone;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategoryById(long id)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDto>();
            try
            {
                var dbCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (dbCategory == null)
                {
                    throw new Exception($"Category cannot find ID : {id}");
                }
                serviceResponse.Data = _mapper.Map<GetCategoryDto>(dbCategory);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto updateCategory)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDto>();
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == updateCategory.Id);
                if(category == null)
                {
                    throw new Exception($"Category not found ID : {updateCategory.Id} ");
                }
                category.Name = updateCategory.Name;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCategoryDto>(category);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;

        }
    }
}
