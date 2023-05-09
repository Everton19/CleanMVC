using CleanMVC.Domain.Entities;
using CleanMVC.Domain.Interfaces;
using CleanMVC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMVC.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Set<Product>().AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            var query = _context.Set<Product>().Where(x => x.Id == id);
            if (await query.AnyAsync())
                return await query.FirstOrDefaultAsync();

            return null;
        }

        public async Task<Product> GetProductCategoryAsync(int? id)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        select p;

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var query = _context.Set<Product>();
            
            return await query.AnyAsync() ? await query.ToListAsync() : new List<Product>();
        }

        public async Task<Product> RemoveAsync(Product product)
        {
            _context.Set<Product>().Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Set<Product>().Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
