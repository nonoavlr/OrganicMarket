using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace MercadoInfrastructure
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine("Enviando email...");
            return Task.CompletedTask;
        }
    }
}
