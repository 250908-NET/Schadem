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

        public async Task DeleteAsync(Order order)
        {
            await _repo.DeleteAsync(order);
            await _repo.SaveChangesAsync();
        }

        public async Task CreateAsync(Order order)
        {
            var attachedProducts = new List<Product>();
            foreach (var product in order.Products)
            {
                var existing = await _repo.GetProductByIdAsync(product.ProductId); // new method in repo
                if (existing != null)
                attachedProducts.Add(existing);
            }

                order.Products = attachedProducts;

            await _repo.AddAsync(order);
            await _repo.SaveChangesAsync();
        }
    }
    
}