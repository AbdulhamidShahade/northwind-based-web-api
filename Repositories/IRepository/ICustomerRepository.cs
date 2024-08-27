using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;

namespace NorthwindBasedWebAPI.Repositories.IRepository
{
    public interface ICustomerRepository : IEntityBaseRepository<Customer>
    {
        Task<ICollection<Order>> GetOrdersByCustomerAsync(int id);
        Task<ICollection<CustomerDemographic>> GetCustomerDemographicsByCustomerAsync(int id);
    }
}
