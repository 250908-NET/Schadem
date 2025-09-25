using Store.Models;
using Store.Repositories;

namespace Store.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Customer>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Customer?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task CreateAsync(Customer customer)
        {
            await _repo.AddAsync(customer);
            await _repo.SaveChangesAsync();
        }
    }
    
}