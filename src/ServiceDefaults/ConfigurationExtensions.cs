namespace Microsoft.Extensions.Configuration;

/// <summary>Configure extensions.</summary>
public static class ConfigurationExtensions
{
	/// <summary>Get required configuration value.</summary>
	/// <param name="configuration">Configuration.</param>
	/// <param name="name">Name of configuration to get.</param>
	/// <returns>Configuration value.</returns>
	/// <exception cref="InvalidOperationException">Configuration missing value.</exception>
	public static string GetRequiredValue(this IConfiguration configuration, string name) =>
		configuration[name] ?? throw new InvalidOperationException($"Configuration missing value for: {(configuration is IConfigurationSection s ? s.Path + ":" + name : name)}");
}