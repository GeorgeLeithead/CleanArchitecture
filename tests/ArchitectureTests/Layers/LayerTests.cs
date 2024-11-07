namespace ArchitectureTests.Layers;

/// <summary>Layer tests.</summary>
public class LayerTests : BaseTest
{
	/// <summary>Application layer - Should not have a dependency on the Infrastructure layer.</summary>
	[Fact]
	public void ApplicationLayerShouldNotHaveDependencyOnInfrastructureLayer()
	{
		TestResult result = Types.InAssembly(applicationAssembly)
			.Should()
			.NotHaveDependencyOn(infrastructureAssembly.GetName().Name)
			.GetResult();

		_ = result.IsSuccessful.Should().BeTrue();
	}

	/// <summary>Application layer - Should not have a dependency on the Presentation layer.</summary>
	[Fact]
	public void ApplicationLayerShouldNotHaveDependencyOnPresentationLayer()
	{
		TestResult result = Types.InAssembly(applicationAssembly)
			.Should()
			.NotHaveDependencyOn(presentationAssembly.GetName().Name)
			.GetResult();

		_ = result.IsSuccessful.Should().BeTrue();
	}

	/// <summary>Domain Layer - Should not have a dependency on the Application.</summary>
	[Fact]
	public void DomainLayerShouldNotHaveDependencyOnApplication()
	{
		TestResult result = Types.InAssembly(domainAssembly)
			.Should()
			.NotHaveDependencyOn("Application")
			.GetResult();

		_ = result.IsSuccessful.Should().BeTrue();
	}

	/// <summary>Domain Layer - Should not have a dependency on the Infrastructure layer.</summary>
	[Fact]
	public void DomainLayerShouldNotHaveDependencyOnInfrastructureLayer()
	{
		TestResult result = Types.InAssembly(domainAssembly)
			.Should()
			.NotHaveDependencyOn(infrastructureAssembly.GetName().Name)
			.GetResult();

		_ = result.IsSuccessful.Should().BeTrue();
	}

	/// <summary>Domain Layer - Should not have a dependency on the Presentation layer.</summary>
	[Fact]
	public void DomainLayerShouldNotHaveDependencyOnPresentationLayer()
	{
		TestResult result = Types.InAssembly(domainAssembly)
			.Should()
			.NotHaveDependencyOn(presentationAssembly.GetName().Name)
			.GetResult();

		_ = result.IsSuccessful.Should().BeTrue();
	}

	/// <summary>Infrastructure Layer - Should not have a dependency on the Presentation layer.</summary>
	[Fact]
	public void InfrastructureLayerShouldNotHaveDependencyOnPresentationLayer()
	{
		TestResult result = Types.InAssembly(infrastructureAssembly)
			.Should()
			.NotHaveDependencyOn(presentationAssembly.GetName().Name)
			.GetResult();

		_ = result.IsSuccessful.Should().BeTrue();
	}
}