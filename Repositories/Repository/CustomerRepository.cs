using Microsoft.EntityFrameworkCore;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;
using NorthwindBasedWebAPI.Data;
using NorthwindBasedWebAPI.Repositories.IRepository;

namespace NorthwindBasedWebApplication.API.Repositories.Repository
{
    public class CustomerRepository : EntityBaseRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<ICollection<CustomerDemographic>> GetCustomerDemographicsByCustomerAsync(int id)
        {
            var customerDemographic = await _context.CustomersCustomerDemographics
                .Where(i => i.CustomerId == id)
                .Select(c => c.CustomerType)
                .OrderBy(i => i.Id)
                .ToListAsync();

            return customerDemographic;
        }

        public async Task<ICollection<Order>> GetOrdersByCustomerAsync(int id)
        {
            var orders = await _context.Orders
                .Where(i => i.CustomerId == id)
                .OrderBy(i => i.Id)
                .ToListAsync();

            return orders;
        }
    }
}
