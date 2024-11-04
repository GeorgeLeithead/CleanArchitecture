namespace Web.Api.Infrastructure;

static class CustomResults
{
	public static IResult Problem(Result result)
	{
		return result.IsSuccess
			? throw new InvalidOperationException()
			: Results.Problem(detail: GetDetail(result.Error), statusCode: GetStatusCode(result.Error.Type), title: GetTitle(result.Error), type: GetType(result.Error.Type), extensions: GetErrors(result));

		static string GetTitle(Error error) =>
			error.Type switch
			{
				ErrorType.Validation => error.Code,
				ErrorType.Problem => error.Code,
				ErrorType.NotFound => error.Code,
				ErrorType.Conflict => error.Code,
				ErrorType.Failure => error.Code,
				_ => "Server failure"
			};

		static string GetDetail(Error error) =>
			error.Type switch
			{
				ErrorType.Validation => error.Description,
				ErrorType.Problem => error.Description,
				ErrorType.NotFound => error.Description,
				ErrorType.Conflict => error.Description,
				ErrorType.Failure => error.Description,
				_ => "An unexpected error occurred"
			};

		static string GetType(ErrorType errorType) =>
			errorType switch
			{
				ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
				ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
				ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
				ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
				ErrorType.Failure => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6",
				_ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
			};

		static int GetStatusCode(ErrorType errorType) =>
			errorType switch
			{
				ErrorType.Validation => StatusCodes.Status400BadRequest,
				ErrorType.NotFound => StatusCodes.Status404NotFound,
				ErrorType.Conflict => StatusCodes.Status409Conflict,
				ErrorType.Failure => StatusCodes.Status500InternalServerError,
				ErrorType.Problem => StatusCodes.Status400BadRequest,
				_ => StatusCodes.Status500InternalServerError
			};

		static Dictionary<string, object?>? GetErrors(Result result) => result.Error is not ValidationError validationError
				? null
				: new Dictionary<string, object?>
					{
						{ "errors", validationError.GetErrors() }
					};
	}
}