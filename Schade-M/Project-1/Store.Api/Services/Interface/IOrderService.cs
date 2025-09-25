using Store.Models;

namespace Store.Services
{
    public interface IOrderService
    {
        public Task<List<Order>> GetAllAsync();
        public Task<Order?> GetByIdAsync(int id);
        public Task DeleteAsync(Order order);
        public Task CreateAsync(Order order);
    }
}