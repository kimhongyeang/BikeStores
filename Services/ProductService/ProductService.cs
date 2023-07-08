using AutoMapper;
using DotNetCore.Data;
using DotNetCore.Dtos;
using DotNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ProductService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            var serviceResponse = new ServiceResponse<List<GetProductDto>>();
            var product = _mapper.Map<Product>(newProduct);
            _context.Add(product);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Products.Select(c => _mapper.Map<GetProductDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(long id)
        {
            var serviceResponse = new ServiceResponse<List<GetProductDto>>();
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
                if (product == null)
                {
                    throw new Exception($"Product cannot find ID : {id} ");
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Products.Select(p => _mapper.Map<GetProductDto>(p)).ToListAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(long id)
        {
            var serviceResponse = new ServiceResponse<GetProductDto>();
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
                if (product == null)
                {
                    throw new Exception($"Product cannot find ID : {id} ");
                }
                serviceResponse.Data = _mapper.Map<GetProductDto>(product);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetProducts()
        {
            var serviceResponse = new ServiceResponse<List<GetProductDto>>();
            var dbProducts = await _context.Products.ToListAsync();
            serviceResponse.Data = dbProducts.Select(p => _mapper.Map<GetProductDto>(p)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updateProduct)
        {
            var serviceResponse = new ServiceResponse<GetProductDto>();
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == updateProduct.Id);
                if (product == null)
                {
                    throw new Exception($"Product cannot find ID : {updateProduct.Id}");
                }
                product.Name = updateProduct.Name;
                product.ModelYear = updateProduct.ModelYear;
                product.ListPrice = updateProduct.ListPrice;
                product.CategoryId = updateProduct.CategoryId;
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetProductDto>(product);
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
