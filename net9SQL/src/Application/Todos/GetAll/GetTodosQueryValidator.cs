namespace Application.Todos.GetAll;

/// <summary>Get to do query validator.</summary>
sealed class GetTodosQueryValidator : AbstractValidator<GetTodosQuery>
{
	public GetTodosQueryValidator()
	{
		_ = RuleFor(c => c.UserId).NotEmpty();
		_ = RuleFor(c => c.page).GreaterThanOrEqualTo(1).WithMessage("Page must be greater than or equal to 1.");
		_ = RuleFor(c => c.pageSize).GreaterThanOrEqualTo(1).WithMessage("Page Size must be greater than or equal to 1.");
	}
}
