﻿using DotNetCore.Dtos;
using DotNetCore.Models;

namespace DotNetCore.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<GetCategoryDto>>> GetCategories();

        Task<ServiceResponse<GetCategoryDto>> GetCategoryById(long id);

        Task<ServiceResponse<List<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory);
    }
}
