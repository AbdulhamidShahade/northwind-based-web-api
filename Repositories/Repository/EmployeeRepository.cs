using Microsoft.EntityFrameworkCore;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;
using NorthwindBasedWebAPI.Data;
using NorthwindBasedWebAPI.Repositories.IRepository;

namespace NorthwindBasedWebApplication.API.Repositories.Repository
{
    public class EmployeeRepository : EntityBaseRepository<Employee>, IEmployeeRepository
    {

        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Order>> GetOrdersByEmployeeAsync(int id)
        {
            var orders = await _context.Orders
                .Where(i => i.EmployeeId == id)
                .OrderBy(i => i.Id)
                .ToListAsync();

            return orders;
        }

        public async Task<ICollection<Territory>> GetTerritoriesByEmployeeAsync(int id)
        {
            var territories = await _context.EmployeesTerritories
                .Where(i => i.EmployeeId == id)
                .Select(t => t.Territory)
                .ToListAsync();

            return territories;
        }
    }
}
