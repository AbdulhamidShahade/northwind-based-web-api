using Microsoft.AspNetCore.Identity;

namespace NorthwindBasedWebAPI.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        private ApplicationRole() { }

        public ApplicationRole(string role) : base(role) 
        {

        }
    }
}
