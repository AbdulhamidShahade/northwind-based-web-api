using Microsoft.EntityFrameworkCore;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;
using NorthwindBasedWebAPI.Data;
using NorthwindBasedWebAPI.Repositories.IRepository;

namespace NorthwindBasedWebApplication.API.Repositories.Repository
{
    public class RegionRepository : EntityBaseRepository<Region>, IRegionRepository
    {

        private readonly ApplicationDbContext _context;

        public RegionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Territory>> GetTerritoriesByRegionAsync(int id)
        {
            var territories = await _context.Territories
                .Where(i => i.RegionId == id)
                .ToListAsync();

            return territories;
        }
    }
}
