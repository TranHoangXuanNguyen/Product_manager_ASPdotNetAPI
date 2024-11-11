using Microsoft.EntityFrameworkCore;
using ProductRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options) { }

        public DbSet<ProductEntity> Products  //table product link vs ProductEntity
        {
            get; set;
        }
    }
}
