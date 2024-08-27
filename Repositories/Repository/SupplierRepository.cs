using Microsoft.EntityFrameworkCore;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;
using NorthwindBasedWebAPI.Data;
using NorthwindBasedWebAPI.Repositories.IRepository;

namespace NorthwindBasedWebApplication.API.Repositories.Repository
{
    public class SupplierRepository : EntityBaseRepository<Supplier>, ISupplierRepository
    {

        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Product>> GetProductsBySupplier(int id)
        {
            var products = await _context.Products
                .Where(i => i.SupplierId == id)
                .ToListAsync();

            return products;
        }
    }
}
