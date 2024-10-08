﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Models.Common;
using NorthwindBasedWebAPI.Models.Common.Authorization;
using NorthwindBasedWebAPI.Repositories.IRepository;
using NorthwindBasedWebApplication.API.Models;

using NorthwindBasedWebApplication.API.Models.Requests;


namespace NorthwindBasedWebApplication.API.Repositories.Repository
{
    public class AuthorizationRepository : IAuthorizationRepository
    {

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;   
        private readonly ILogger<AuthorizationRepository> _logger;
        private readonly IMapper _mapper;

        public AuthorizationRepository(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager,
            ILogger<AuthorizationRepository> logger,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> AddRoleAsync(string roleName)
        {
            var created = await _roleManager.CreateAsync(new ApplicationRole(roleName));

            return created.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString()); 

            if(role == null)
            {
                return false;
            }


            var result = await _roleManager.DeleteAsync(role);

            return result.Succeeded;
        }

        public async Task<IReadOnlyList<RoleDto>> GetAllAsync()
        {
            return _mapper.Map<IReadOnlyList<RoleDto>>(await _roleManager.Roles.ToListAsync());
        }

        public async Task<ApplicationRole> GetByIdAsync(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<UserClaimsResponse> GetClaimsByUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if(user == null)
            {
                return null;
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = ClaimsStore.claims.Select(claim => new UserClaims
            {
                Type = claim.Type,
                Value = userClaims.Any(uc => uc.Type == claim.Type)
            }).ToList();

            return new UserClaimsResponse
            {
                UserId = id,
                Claims = claims
            };
        }

        public async Task<UserRolesResponse> GetRolesByUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if(user == null)
            {
                return null;
            }

            var roles = new List<UserRole>();
            var response = new UserRolesResponse();

            var userRoles = await _userManager.GetRolesAsync(user);

            var rolesList = await _roleManager.Roles.ToListAsync();
            response.UserId = id;

            foreach(var role in rolesList)
            {
                roles.Add(new UserRole
                {
                    Id = Convert.ToInt32(role.Id),
                    Name = role.Name,
                    HasRole = userRoles.Contains(role.Name)
                });
            }

            response.Roles = roles;
            return response;
        }


        public async Task<UserRolesResponse> GetRolesByUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return null;
            }
            
            var roles = new List<UserRole>();

            var response = new UserRolesResponse();

            var userRoles = await _userManager.GetRolesAsync(user);

            var rolesList = await _roleManager.Roles.ToListAsync();

            response.UserId = user.Id;

            foreach(var role in rolesList)
            {
                roles.Add(new UserRole
                {
                    Id = Convert.ToInt32(role.Id),
                    Name = role.Name,
                    HasRole = userRoles.Contains(role.Name)

                });
            }

            response.Roles = roles;

            return response;

        }

        public async Task<List<string>> GetRolesNamesByUser(int id)
        {
            return GetRolesByUser(id).GetAwaiter().GetResult().Roles.Where(h => h.HasRole).Select(n => n.Name).ToList();
        }

        public async Task<List<string>> GetRolesNamesByUser(string email)
        {
            return GetRolesByUser(email).GetAwaiter().GetResult().Roles.Where(h => h.HasRole).Select(n => n.Name).ToList();
        }

        public async Task<bool> IsExistsAsync(string role)
        {
            return await _roleManager.RoleExistsAsync(role);
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString()) != null;
        }

        public async Task<bool> UpdateRoleAsync(UpdateRoleRequest updateRoleRequest)
        {
            var role = await _roleManager.FindByIdAsync(updateRoleRequest.Id.ToString());

            if(role == null)
            {
                return false;
            }

            role.Name = updateRoleRequest.RoleName;

            var result = await _roleManager.UpdateAsync(role);

            return result.Succeeded;
        }

        
    }
}
