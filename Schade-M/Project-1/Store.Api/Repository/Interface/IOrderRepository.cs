using Store.Models;

namespace Store.Repositories
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetAllAsync();
        public Task<Order?> GetByIdAsync(int id);
        public Task AddAsync(Order order);
        public Task DeleteAsync(Order order);
        public Task<Product?> GetProductByIdAsync(int id);
        public Task SaveChangesAsync();
    }
}