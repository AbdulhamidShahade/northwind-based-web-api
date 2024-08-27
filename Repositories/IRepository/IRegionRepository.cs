using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;

namespace NorthwindBasedWebAPI.Repositories.IRepository
{
    public interface IRegionRepository : IEntityBaseRepository<Region>
    {
        Task<ICollection<Territory>> GetTerritoriesByRegionAsync(int id);
    }
}
