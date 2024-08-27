using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.Base;

namespace NorthwindBasedWebAPI.Repositories.IRepository
{
    public interface ICustomerDemographicRepository : IEntityBaseRepository<CustomerDemographic>
    {
        Task<ICollection<Customer>> GetCustomersByCustomerDemographicAsync(int id);
    }
}
