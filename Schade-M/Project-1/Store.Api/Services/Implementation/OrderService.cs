using Store.Models;
using Store.Repositories;

namespace Store.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }
        
        public async Task<List<Order>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Order?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task CreateAsync(Order order)
        {
            await _repo.AddAsync(order);
            await _repo.SaveChangesAsync();
        }
    }
    
}