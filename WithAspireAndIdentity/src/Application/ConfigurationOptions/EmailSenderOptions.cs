namespace Application.ConfigurationOptions;

public class EmailSenderOptions
{
	public required string HostAddress { get; set; }

	public int HostPort { get; set; }

	public required string HostUsername { get; set; }

	public required string HostPassword { get; set; }

	public required string SenderEMail { get; set; }

	public required string SenderName { get; set; }
}