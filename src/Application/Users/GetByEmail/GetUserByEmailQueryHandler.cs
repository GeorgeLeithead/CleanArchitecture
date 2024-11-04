namespace Application.Users.GetByEmail;

sealed class GetUserByEmailQueryHandler(IApplicationDbContext context, IUserContext userContext) : IQueryHandler<GetUserByEmailQuery, UserResponse>
{
	public async Task<Result<UserResponse>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
	{
		UserResponse? user = await context.Users
			.Where(u => u.Email == query.Email)
			.Select(u => new UserResponse
			{
				Id = u.Id,
				FirstName = u.FirstName,
				LastName = u.LastName,
				Email = u.Email
			})
			.SingleOrDefaultAsync(cancellationToken);

		return user switch
		{
			null => Result.Failure<UserResponse>(UserErrors.NotFoundByEmail),
			_ => user.Id != userContext.UserId ? Result.Failure<UserResponse>(UserErrors.Unauthorized()) : (Result<UserResponse>)user,
		};
	}
}