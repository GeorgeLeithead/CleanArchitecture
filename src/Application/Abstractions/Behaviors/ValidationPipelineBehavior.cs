// Ignore Spelling: nullable

namespace Application.Abstractions.Behaviors;

sealed class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : class
{
	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(next);

		ValidationFailure[] validationFailures = await ValidateAsync(request);
		if (validationFailures.Length == 0)
		{
			return await next().ConfigureAwait(false);
		}

		if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
		{
			Type resultType = typeof(TResponse).GetGenericArguments()[0];
			MethodInfo? failureMethod = typeof(Result<>)
				.MakeGenericType(resultType)
				.GetMethod(nameof(Result<object>.ValidationFailure));

			if (failureMethod is not null)
			{
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
				return (TResponse)failureMethod.Invoke(null, [CreateValidationError(validationFailures)]);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8603 // Possible null reference return.
			}
		}
		else if (typeof(TResponse) == typeof(Result))
		{
			return (TResponse)(object)Result.Failure(CreateValidationError(validationFailures));
		}

		throw new ValidationException(validationFailures);
	}

	/// <summary>Create validation error.</summary>
	/// <param name="validationFailures">A list of <see cref="ValidationFailure"/> errors.</param>
	/// <returns>A <see cref="ValidationError"/>.</returns>
	static ValidationError CreateValidationError(ValidationFailure[] validationFailures) => new(validationFailures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage)).ToArray());

	async Task<ValidationFailure[]> ValidateAsync(TRequest request)
	{
		if (!validators.Any())
		{
			return [];
		}

		ValidationContext<TRequest> context = new(request);

		ValidationResult[] validationResults = await Task.WhenAll(validators.Select(validator => validator.ValidateAsync(context))).ConfigureAwait(false);

		ValidationFailure[] validationFailures = validationResults
			.Where(validationResult => !validationResult.IsValid)
			.SelectMany(validationResult => validationResult.Errors)
			.ToArray();

		return validationFailures;
	}
}