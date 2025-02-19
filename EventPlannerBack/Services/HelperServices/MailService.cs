using System.Net.Mail;
using EventPlannerBack.Interfaces.Services;
using EventPlannerBack.Models.Configuration;
using Microsoft.Extensions.Options;

namespace EventPlannerBack.Services.HelperServices;

public class MailService : IMailService
{
    public readonly GmailOptions options;

    public MailService(IOptions<GmailOptions> options)
    {
        this.options = options.Value;
    }
    public async Task<bool> SendAuthMail(string to, string subject, string body)
    {
        try
        {
            var content = "";
            using (var reader = new StreamReader("main.html"))
            {
                content = await reader.ReadToEndAsync();
                content = content.Replace("AuthenticationCode", body);
            }
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(options.Email),
                Subject = subject,
                Body = content
            };
            mail.To.Add(to);
            mail.IsBodyHtml = true;
            using (var smtclinet = new SmtpClient())
            {
                smtclinet.Host = options.Host;
                smtclinet.Port = options.Port;
                smtclinet.Timeout = 1000;
                smtclinet.EnableSsl = true;
                smtclinet.Credentials = new System.Net.NetworkCredential(options.Email, options.Password);

                await smtclinet.SendMailAsync(mail);
                return true;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}