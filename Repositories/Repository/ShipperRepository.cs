using Microsoft.EntityFrameworkCore;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;
using NorthwindBasedWebAPI.Data;
using NorthwindBasedWebAPI.Repositories.IRepository;

namespace NorthwindBasedWebApplication.API.Repositories.Repository
{
    public class ShipperRepository : EntityBaseRepository<Shipper>, IShipperRepository
    {

        private readonly ApplicationDbContext _context;


        public ShipperRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Order>> GetOrdersByShipper(int id)
        {
            var orders = await _context.Orders
                .Where(i => i.ShipVia == id)
                .ToListAsync();

            return orders;
        }
    }
}
