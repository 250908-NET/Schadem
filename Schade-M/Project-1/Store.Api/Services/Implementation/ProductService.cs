using Store.Models;
using Store.Repositories;

namespace Store.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Product>> GetAllAsync() => await _repo.GetAllAsync();
        
        public async Task<Product?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
        
        public async Task CreateAsync(Product product)
        {
            await _repo.AddAsync(product);
            await _repo.SaveChangesAsync();
        }
    }
    
}