using Store.Models;
using Store.Data;
using Microsoft.EntityFrameworkCore;

namespace Store.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext _context;

        public OrderRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllAsync()
        {
           return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            //return await _context.Order.Where( student => student.id  == id);
            //throw new NotImplementedException();
            return await _context.Orders
                        .Include(p => p.Products) 
                        .FirstOrDefaultAsync(p => p.OrderId == id); 
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            await Task.CompletedTask; 
        }


        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
