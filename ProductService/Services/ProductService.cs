using ProductRepository.Entities;
using ProductRepository.Interfaces;
using ProductService.Interfaces;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<long> CreateProductAsync(CreateProductDTO request)
        {
            // Call the repository to add the product to the database
            var productEntity = new ProductEntity();
            productEntity.Name = request.Name;
            productEntity.Description = request.Description;
            productEntity.Price = request.Price;
            productEntity.CreatedTime = DateTime.Now;
            productEntity.UpdatedTime = null;
            return await _productRepository.AddProductAsync(productEntity);
            
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            var listProductEntity =  await _productRepository.GetAllProductsAsync();
            return listProductEntity.Select(x => new ProductDTO
            {
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
            }).ToList();
        }

        public async Task<GetProductDTO> GetProductByIdAsync(int id)
        {
            var getProductDTO = new GetProductDTO();
            var entityFormDB = await _productRepository.GetProductByIdAsync(id);
            getProductDTO.Id = id;
            getProductDTO.Name = entityFormDB.Name;
            getProductDTO.Description = entityFormDB.Description;
            getProductDTO.Price = entityFormDB.Price;
            getProductDTO.UpdatedTime = entityFormDB.UpdatedTime;
            return getProductDTO;
        }

        public async Task UpdateProductAsync(GetProductDTO request)
        {
            var productEntity = new ProductEntity();
            productEntity.Id = request.Id;
            productEntity.Name = request.Name;
            productEntity.Description = request.Description;
            productEntity.Price = request.Price;
            productEntity.UpdatedTime = DateTime.Now;
            await _productRepository.UpdateProductAsync(productEntity);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
        }
    }

}
