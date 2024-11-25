using Microsoft.AspNetCore.Mvc;
using ProductService.Services;
using ProductRepository.Entities;
using ProductService.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductService.Models;

namespace YourWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name) || request.Price <= 0)
            {
                return BadRequest("Invalid product data.");
            }

            var newid = await _productService.CreateProductAsync(request);

            return Ok(newid);  // Return created product with id
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();  // Assuming a method to get all products
            if (products == null || products.Count == 0)
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);  // Assuming a method to get product by ID
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(product);
        }

        // PUT: api/product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] GetProductDTO request)
        {
            // Tìm sản phẩm trong cơ sở dữ liệu
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            // Cập nhật các trường dữ liệu của sản phẩm
            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.Price = request.Price;
            existingProduct.UpdatedTime = DateTime.UtcNow; // Cập nhật thời gian sửa

            // Lưu thay đổi vào cơ sở dữ liệu
            await _productService.UpdateProductAsync(existingProduct);

            return NoContent(); // Trả về trạng thái thành công mà không cần dữ liệu
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            await _productService.DeleteProductAsync(id);  // Assuming a method to delete product
            return NoContent();  // 204 No Content indicates successful deletion
        }
    }
}
