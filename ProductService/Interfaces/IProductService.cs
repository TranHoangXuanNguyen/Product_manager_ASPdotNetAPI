using ProductRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductEntity product);
        Task<List<ProductEntity>> GetAllProductsAsync();
        Task<ProductEntity> GetProductByIdAsync(int id);
        Task UpdateProductAsync(ProductEntity product);
        Task DeleteProductAsync(int id);
    }
}
