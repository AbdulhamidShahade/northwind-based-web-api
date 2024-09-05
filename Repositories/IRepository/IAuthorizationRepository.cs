using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Models.Common;
using NorthwindBasedWebApplication.API.Models;



namespace NorthwindBasedWebAPI.Repositories.IRepository
{
    public interface IAuthorizationRepository
    {
        Task<bool> AddRoleAsync(string roleName);
        Task<bool> UpdateRoleAsync(Models.Common.Authorization.UpdateRoleRequest updateRoleRequest);
        Task<bool> DeleteRoleAsync(int id);
        Task<IReadOnlyList<RoleDto>> GetAllAsync();
        Task<ApplicationRole> GetByIdAsync(int id);
        Task<bool> IsExistsAsync(string role);
        Task<bool> IsExistsAsync(int id);

        Task<UserRolesResponse> GetRolesByUser(int id);
        Task<UserRolesResponse> GetRolesByUser(string email);
        Task<List<string>> GetRolesNamesByUser(int id);
        Task<List<string>> GetRolesNamesByUser(string email);

        Task<UserClaimsResponse> GetClaimsByUser(int id);
        
    }
}
