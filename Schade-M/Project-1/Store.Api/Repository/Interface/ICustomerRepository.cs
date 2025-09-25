using Store.Models;

namespace Store.Repositories
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> GetAllAsync();
        public Task<Customer?> GetByIdAsync(int id);
        public Task AddAsync(Customer customer);
        public Task SaveChangesAsync();
    }
}