using API.DTOs.Requests;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace API.Utilities;

public interface IEmailHandler
{
    Task SendEmailAsync(EmailRequestDto emailDto);
}

public class EmailHandler : IEmailHandler
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _mailFrom;
    private readonly string _mailName;

    public EmailHandler(string smtpServer, int smtpPort, string mailFrom, string mailName)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _mailFrom = mailFrom;
        _mailName = mailName;
    }

    public async Task SendEmailAsync(EmailRequestDto emailDto)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_mailName, _mailFrom));
        emailMessage.To.Add(new MailboxAddress(emailDto.FullName, emailDto.To));
        emailMessage.Subject = emailDto.Subject;

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = emailDto.Body;
        emailMessage.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        const SecureSocketOptions secureSocketOptions = SecureSocketOptions.None;
        await client.ConnectAsync(_smtpServer, _smtpPort, secureSocketOptions);
        //await client.AuthenticateAsync("", "");
        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}

