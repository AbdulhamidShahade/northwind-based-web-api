using Microsoft.EntityFrameworkCore;
using NorthwindBasedWebAPI.Data;
using NorthwindBasedWebAPI.Repositories.IRepository;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;


namespace NorthwindBasedWebApplication.API.Repositories.Repository
{
    public class TerritoryRepository : EntityBaseRepository<Territory>, ITerritoryRepository
    {

        private readonly ApplicationDbContext _context;

        public TerritoryRepository(ApplicationDbContext context) : base (context)
        {
            _context = context;
        }

        public async Task<ICollection<Employee>> GetEmployeesByTerritoryAsync(int id)
        {
            var employees = await _context.EmployeesTerritories
                .Where(i => i.TerritoryId == id)
                .Select(e => e.Employee)
                .ToListAsync();

            return employees;
        }

        public async Task<Region> GetRegionByTerritoryAsync(int id)
        {
            var region = await _context.Territories
                .Where(i => i.Id == id)
                .Select(r => r.Region)
                .FirstOrDefaultAsync();

            return region;
        }
    }
}
