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
            var listProductEntity = await _productRepository.GetAllProductsAsync();
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
            if (entityFormDB != null)
            {
                getProductDTO.Id = id;
                getProductDTO.Name = entityFormDB.Name;
                getProductDTO.Description = entityFormDB.Description;
                getProductDTO.Price = entityFormDB.Price;
                getProductDTO.UpdatedTime = entityFormDB.UpdatedTime;
            }

            return getProductDTO;
        }

        public async Task<bool> UpdateProductAsync(int id, UpdateProductDTO request)
        {
            // Tìm sản phẩm trong cơ sở dữ liệu
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                throw new Exception($"product not existing with id : {id}");
            }
            // Cập nhật các trường dữ liệu của sản phẩm
            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.Price = request.Price;
            existingProduct.UpdatedTime = DateTime.Now;


            return await _productRepository.UpdateProductAsync(existingProduct);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                throw new Exception($"product not existing with id : {id}");
            }
            return await _productRepository.DeleteProductAsync(id);
        }
    }

}
