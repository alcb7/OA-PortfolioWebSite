using OA.PortfolioWebSite.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OA.PortfolioWebSite.Application.Interfaces.Services;
using OA.PortfolioWebSite.Persistance.Contexts;
using OA.PortfolioWebSite.Application.DTOs;

namespace OA.PortfolioWebSite.Persistance.Services
{
    public class SendContactService : ISendContactService
    {
        private readonly DataAPIDbContext _dbContext;
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _receiverEmail;

        public SendContactService(DataAPIDbContext dbContext, string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, string receiverEmail)
        {
            _dbContext = dbContext;
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
            _receiverEmail = receiverEmail;
        }

        public async Task SendContactMessageAsync(SendContactDto contactMessage)
        {
            // Gönderim tarihini belirliyoruz
            var contactMessageEntity = new ContactMessages
            {
                Name = contactMessage.Name,
                Email = contactMessage.Email,
                Subject = contactMessage.Subject,
                Message = contactMessage.Message,
                SentDate = DateTime.UtcNow,
                IsRead = false,
                Reply = contactMessage.Reply,
                ReplyDate = contactMessage.ReplyDate
            };

            // Entity'yi veritabanına ekleme
            _dbContext.ContactMessages.Add(contactMessageEntity);
            await _dbContext.SaveChangesAsync();

            // Kullanıcının mesajını belirttiğiniz adrese gönderme
            using (var client = new SmtpClient(_smtpServer, _smtpPort)
            {
                Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                EnableSsl = true
            })
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(contactMessage.Email),
                    Subject = contactMessage.Subject,
                    Body = $"Name: {contactMessage.Name}\nEmail: {contactMessage.Email}\n\nMessage:\n{contactMessage.Message}"
                };
                mailMessage.To.Add(_receiverEmail);

                await client.SendMailAsync(mailMessage);

                // Kullanıcıya geri dönüş maili
                var confirmationMail = new MailMessage
                {
                    From = new MailAddress(_smtpUsername),
                    Subject = "Your message has been received",
                    Body = "Thank you for reaching out. We have received your message and will get back to you shortly."
                };
                confirmationMail.To.Add(contactMessage.Email);

                await client.SendMailAsync(confirmationMail);
            }
        }
    }
}
