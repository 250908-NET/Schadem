using Store.Models;
using Store.Data;
using Microsoft.EntityFrameworkCore;

namespace Store.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
           return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            //return await _context.Product.Where( student => student.id  == id);
            //return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            throw new NotImplementedException();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
