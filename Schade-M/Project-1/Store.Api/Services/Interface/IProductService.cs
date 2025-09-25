using Store.Models;

namespace Store.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetAllAsync();
        public Task<Product?> GetByIdAsync(int id);
        public Task CreateAsync(Product product);
    }
}