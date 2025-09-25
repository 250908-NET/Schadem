using Store.Models;

namespace Store.Services
{
    public interface ICustomerService
    {
        public Task<List<Customer>> GetAllAsync();
        public Task<Customer?> GetByIdAsync(int id);
        public Task DeleteAsync(Customer customer);
        public Task CreateAsync(Customer customer);
    }
}