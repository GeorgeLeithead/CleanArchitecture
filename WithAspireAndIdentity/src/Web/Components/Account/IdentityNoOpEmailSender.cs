namespace Web.Components.Account;

// Remove the "else if (EmailSender is IdentityNoOpEmailSender)" block from RegisterConfirmation.razor after updating with a real implementation.
sealed class IdentityNoOpEmailSender : IEmailSender<AppUser>
{
	readonly IEmailSender emailSender = new NoOpEmailSender();

	public Task SendConfirmationLinkAsync(AppUser user, string email, string confirmationLink)
	{
		return emailSender.SendEmailAsync(email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");
	}

	public Task SendPasswordResetLinkAsync(AppUser user, string email, string resetLink)
	{
		return emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");
	}

	public Task SendPasswordResetCodeAsync(AppUser user, string email, string resetCode)
	{
		return emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password using the following code: {resetCode}");
	}
}
