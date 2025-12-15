using Microsoft.AspNetCore.Identity;

namespace IMS.Plugins.EFCoreSqlServer
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Fullname => $"{FirstName} {LastName}";
    }
}