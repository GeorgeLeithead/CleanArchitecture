namespace AspireTests;

public class WebTests
{
	[Fact]
	public async Task GetWebResourceRootReturnsOkStatusCode()
	{
		// Arrange
		IDistributedApplicationTestingBuilder appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.AppHost>();
		_ = appHost.Services.ConfigureHttpClientDefaults(clientBuilder => _ = clientBuilder.AddStandardResilienceHandler());
		// To output logs to the xUnit.net ITestOutputHelper, consider adding a package from https://www.nuget.org/packages?q=xunit+logging

		await using Aspire.Hosting.DistributedApplication app = await appHost.BuildAsync();
		ResourceNotificationService resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
		await app.StartAsync();

		// Act
		HttpClient httpClient = app.CreateHttpClient("webfrontend");
		await resourceNotificationService.WaitForResourceAsync("webfrontend", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));
		HttpResponseMessage response = await httpClient.GetAsync("/");

		// Assert
		Assert.Equal(HttpStatusCode.OK, response.StatusCode);
	}
}
