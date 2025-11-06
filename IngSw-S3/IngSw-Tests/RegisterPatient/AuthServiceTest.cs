using IngSw_Application.Services;
using IngSw_Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace IngSw_Tests.RegisterPatient;

public class AuthServiceTest
{
	private readonly IAuthRepository _authRepository;
	private readonly AuthService _authService;
	public AuthServiceTest(IConfiguration configuration)
	{
		_authRepository = Substitute.For<IAuthRepository>();
		_authService = new AuthService(_authRepository, configuration);
	}


}
