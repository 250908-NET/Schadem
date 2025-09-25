using Store.Models;
using Store.Data;
using Microsoft.EntityFrameworkCore;

namespace Store.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StoreDbContext _context;

        public CustomerRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
           return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            //return await _context.Customer.Where( student => student.id  == id);
            //return await _context.Products();
            //throw new NotImplementedException();
            return await _context.Customers
                        .Include(c => c.Orders) 
                        .FirstOrDefaultAsync(c => c.Id == id); 
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
