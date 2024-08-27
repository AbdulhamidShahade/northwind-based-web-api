using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;

namespace NorthwindBasedWebAPI.Repositories.IRepository
{
    public interface IProductRepository : IEntityBaseRepository<Product>
    {
        Task<Category> GetCategoryByProductAsync(int id);
        Task<Supplier> GetSupplierByProductAsync(int id);
        Task<ICollection<Order>> GetOrdersByProductAsync(int id);
    }
}
