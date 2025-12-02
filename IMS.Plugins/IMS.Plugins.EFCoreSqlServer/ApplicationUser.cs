using Microsoft.AspNetCore.Identity;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class ApplicationUser : IdentityUser
    {
        // Add custom properties if needed
        public string? FullName { get; set; }
    }
}