﻿using Microsoft.EntityFrameworkCore;
using ProductRepository.Entities;
using ProductRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDBContext _context;

        public ProductRepository(ProductDBContext context)
        {
            _context = context;
        }

        public async Task<long> AddProductAsync(ProductEntity product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<List<ProductEntity>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ProductEntity>? GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);

        }

        public async Task<bool> UpdateProductAsync(ProductEntity product)
        {
            var Updatedproduct = _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Updatedproduct != null;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }

}
