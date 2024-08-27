using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;

namespace NorthwindBasedWebAPI.Repositories.IRepository
{
    public interface IShipperRepository : IEntityBaseRepository<Shipper>
    {
        Task<ICollection<Order>> GetOrdersByShipper(int id);
    }
}
