using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;

namespace NorthwindBasedWebAPI.Repositories.IRepository
{
    public interface ITerritoryRepository : IEntityBaseRepository<Territory>
    {
        Task<ICollection<Employee>> GetEmployeesByTerritoryAsync(int id);
        Task<Region> GetRegionByTerritoryAsync(int id);
    }
}
