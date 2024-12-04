// Ignore Spelling: jwt

namespace Infrastructure.Authentication;

using Domain.Users;

sealed class TokenProvider(IConfiguration configuration) : ITokenProvider
{
	public string Create(User user)
	{
		string secretKey = Environment.GetEnvironmentVariable("Jwt_Secret") ?? throw new ArgumentException("JWT key is not configured.");
		SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secretKey));
		SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

		DateTime now = DateTime.UtcNow;
		string issuer = configuration["Jwt:Issuer"] ??= string.Empty;
		string audience = configuration["Jwt:Audience"] ??= string.Empty;
		Dictionary<string, object> claims = new()
		{
			{ JwtRegisteredClaimNames.Sub, user.Id.ToString() }, // Subject - See https://datatracker.ietf.org/doc/html/rfc7519#section-4.1.2
			{ JwtRegisteredClaimNames.Email, user.Email },
			{ JwtRegisteredClaimNames.GivenName, user.FirstName },
			{ JwtRegisteredClaimNames.FamilyName, user.LastName },
			{ JwtRegisteredClaimNames.Exp, EpochTime.GetIntDate(now.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes"))) }, // Expiration time - See https://datatracker.ietf.org/doc/html/rfc7519#section-4.1.4
			{ JwtRegisteredClaimNames.Nbf, EpochTime.GetIntDate(now) }, // Not before - See https://datatracker.ietf.org/doc/html/rfc7519#section-4.1.5
			{ JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(now) }, // Issued at - See https://datatracker.ietf.org/doc/html/rfc7519#section-4.1.6
			{ JwtRegisteredClaimNames.Iss, issuer }, // Issuer - See https://datatracker.ietf.org/doc/html/rfc7519#section-4.1.1
			{ JwtRegisteredClaimNames.Aud, audience } // Audience - See https://datatracker.ietf.org/doc/html/rfc7519#section-4.1.3
		};

		SecurityTokenDescriptor tokenDescriptor = new()
		{
			Subject = new ClaimsIdentity(
			[
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email)
			]),
			Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
			SigningCredentials = credentials,
			Issuer = configuration["Jwt:Issuer"],
			Audience = configuration["Jwt:Audience"],
			TokenType = JwtHeaderParameterNames.Jwk,
			Claims = claims
		};

		JsonWebTokenHandler handler = new();

		return handler.CreateToken(tokenDescriptor);
	}
}