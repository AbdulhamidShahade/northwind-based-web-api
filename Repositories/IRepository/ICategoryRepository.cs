

using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;

namespace NorthwindBasedWebAPI.Repositories.IRepository
{
    public interface ICategoryRepository : IEntityBaseRepository<Category>
    {
        Task<ICollection<Product>> GetProductsByCategory(int id);
    }
}
