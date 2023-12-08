using Microsoft.AspNetCore.Identity.UI.Services;

namespace FPTBookShopWeb.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //Thực hiện gửi mail ở đây
            return Task.CompletedTask;
        }
    }
}
