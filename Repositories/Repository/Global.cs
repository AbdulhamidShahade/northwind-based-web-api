using Microsoft.AspNetCore.Identity;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Repositories.IRepository;

namespace NorthwindBasedWebAPI.Repositories.Repository
{
    public class Global : IGlobal
    {
        public string Email { get; set; } = "Anonymous";
        public string Role { get; set; } = "Undefined";
    }
}
