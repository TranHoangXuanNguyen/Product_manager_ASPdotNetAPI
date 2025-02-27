using ProductRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Interfaces
{
    public interface IProductRepository
    {
        Task<long> AddProductAsync(ProductEntity productEntity);
        Task<List<ProductEntity>> GetAllProductsAsync();
        Task<ProductEntity>? GetProductByIdAsync(int id);
        Task<bool> UpdateProductAsync(ProductEntity product);
        Task<bool> DeleteProductAsync(int id);
    }
}
