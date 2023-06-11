using DotNetCore.Dtos;
using DotNetCore.Models;
using DotNetCore.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.Controllers
{
    //*********** Route ************//
    [ApiController]
    [Route("api/[controller]")]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> Get()
        {
            return Ok(await _categoryService.GetCategories());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> GetById(long id)
        {
            return Ok(await _categoryService.GetCategoryById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCategoryDto>>> AddCategory(AddCategoryDto newCategory)
        {
            return Ok(await _categoryService.AddCategory(newCategory));
        }
    }
}
