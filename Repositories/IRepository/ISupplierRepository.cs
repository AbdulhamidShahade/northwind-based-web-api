using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;

namespace NorthwindBasedWebAPI.Repositories.IRepository
{
    public interface ISupplierRepository : IEntityBaseRepository<Supplier>
    {
        Task<ICollection<Product>> GetProductsBySupplier(int id);
    }
}
