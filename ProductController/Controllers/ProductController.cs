using Microsoft.AspNetCore.Mvc;
using ProductService.Services;
using ProductRepository.Entities;
using ProductService.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> CreateProduct([FromBody] ProductEntity product)
        {
            if (product == null || string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0)
            {
                return BadRequest("Invalid product data.");
            }

            await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);  // Return created product with 201 status
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductEntity>>> GetAllProducts()
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
        public async Task<ActionResult<ProductEntity>> GetProductById(int id)
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
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductEntity product)
        {
            if (product == null || id != product.Id)
            {
                return BadRequest("Product ID mismatch.");
            }
            // Tìm sản phẩm trong cơ sở dữ liệu
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            // Cập nhật các trường dữ liệu của sản phẩm
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
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
