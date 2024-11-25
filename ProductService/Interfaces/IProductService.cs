using ProductRepository.Entities;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Interfaces
{
    public interface IProductService
    {
        Task<long> CreateProductAsync(CreateProductDTO product);
        Task<List<ProductDTO>> GetAllProductsAsync();
        Task<GetProductDTO> GetProductByIdAsync(int id);
        Task UpdateProductAsync(GetProductDTO product);
        Task DeleteProductAsync(int id); 
    }
}
