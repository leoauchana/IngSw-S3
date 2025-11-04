using IngSw_Application.DTOs;

namespace IngSw_Application.Interfaces;

public interface IAuthService
{
    Task<UserDto.Response> Login(UserDto.Request userDto);
}
