using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;

namespace NorthwindBasedWebAPI.Repositories.IRepository
{
    public interface IEmployeeRepository : IEntityBaseRepository<Employee>
    {
        Task<ICollection<Territory>> GetTerritoriesByEmployeeAsync(int id);
        Task<ICollection<Order>> GetOrdersByEmployeeAsync(int id);
    }
}
