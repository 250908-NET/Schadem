using Store.Models;

namespace Store.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllAsync();
        public Task<Product?> GetByIdAsync(int id);
        public Task AddAsync(Product product);
        public Task DeleteAsync(Product product);
        public Task SaveChangesAsync();
    }
}