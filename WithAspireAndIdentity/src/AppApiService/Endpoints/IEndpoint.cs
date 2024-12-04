namespace AppApiService.Endpoints;

interface IEndpoint
{
	void MapEndpoint(IEndpointRouteBuilder app);
}