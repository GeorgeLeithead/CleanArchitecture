namespace Application.Users.GetById;

sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
	public GetUserByIdQueryValidator() => RuleFor(c => c.UserId).NotEmpty().WithMessage("User identifier is required.");
}