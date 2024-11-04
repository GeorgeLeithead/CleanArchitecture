namespace Application.Users.GetById;

sealed class GetUserByIdQueryHandler(IApplicationDbContext context, IUserContext userContext) : IQueryHandler<GetUserByIdQuery, UserResponse>
{
	public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
	{
		if (query.UserId != userContext.UserId)
		{
			return Result.Failure<UserResponse>(UserErrors.Unauthorized());
		}

		UserResponse? user = await context.Users
			.Where(u => u.Id == query.UserId)
			.Select(u => new UserResponse
			{
				Id = u.Id,
				FirstName = u.FirstName,
				LastName = u.LastName,
				Email = u.Email
			})
			.SingleOrDefaultAsync(cancellationToken);

		return user is null ? Result.Failure<UserResponse>(UserErrors.NotFound(query.UserId)) : (Result<UserResponse>)user;
	}
}