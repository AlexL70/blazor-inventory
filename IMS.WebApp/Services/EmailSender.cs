using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using IMS.Plugins.EFCoreSqlServer;

namespace IMS.WebApp.Services
{
    public class EmailSender : IEmailSender<ApplicationUser>, IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // TODO: Implement actual email sending logic (e.g., SendGrid, SMTP)
            // For now, just log to console
            Console.WriteLine($"=== Email ===");
            Console.WriteLine($"To: {email}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {htmlMessage}");
            Console.WriteLine($"=============");
            return Task.CompletedTask;
        }

        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            return SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");
        }

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {
            return SendEmailAsync(email, "Reset your password",
                $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");
        }

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            return SendEmailAsync(email, "Reset your password",
                $"Please reset your password using the following code: {resetCode}");
        }
    }
}
