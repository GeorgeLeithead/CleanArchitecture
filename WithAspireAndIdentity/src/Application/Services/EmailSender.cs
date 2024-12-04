namespace Application.Services;

public class EmailSender(IOptions<EmailSenderOptions> options, SmtpClient smtpClient) : IEmailSender
{
	public EmailSenderOptions Options { get; set; } = options.Value;

	public async Task SendEmailAsync(string email, string subject, string htmlMessage)
	{
		await Execute(email, subject, htmlMessage);
	}

	async Task<bool> Execute(string email, string subject, string htmlMessage)
	{
		MailMessage mailMessage = new()
		{
			From = new MailAddress(Options.SenderEMail, Options.SenderName),
			Subject = subject,
			Body = htmlMessage,
			IsBodyHtml = true
		};
		mailMessage.To.Add(email);

		await smtpClient.SendMailAsync(mailMessage);
		return true;
	}
}
