using DotNetCore.Dtos;
using DotNetCore.Models;

namespace DotNetCore.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<GetProductDto>>> GetProducts();

        Task<ServiceResponse<GetProductDto>> GetProductById(long id);

        Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct);

        Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updateProduct);

        Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(long id);
    }
}
