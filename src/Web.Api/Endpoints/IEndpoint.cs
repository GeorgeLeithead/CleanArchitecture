namespace Web.Api.Endpoints;

interface IEndpoint
{
	void MapEndpoint(IEndpointRouteBuilder app);
}