namespace ArchitectureTests;

#pragma warning disable CA1515 // Consider making public types internal
/// <summary>Base test</summary>
/// <remarks>Load the various layers.</remarks>
public abstract class BaseTest
#pragma warning restore CA1515 // Consider making public types internal
{
	/// <summary>Domain Layer assembly.</summary>
	protected static readonly Assembly domainAssembly = typeof(AppUser).Assembly;

	/// <summary>Application Layer assembly.</summary>
	protected static readonly Assembly applicationAssembly = typeof(ICommand).Assembly;

	/// <summary>Infrastructure Layer assembly.</summary>
	protected static readonly Assembly infrastructureAssembly = typeof(ApplicationDbContext).Assembly;

	/// <summary>Presentation layer assembly.</summary>
	protected static readonly Assembly presentationAssembly = typeof(Program).Assembly;
}