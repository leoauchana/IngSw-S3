using IngSw_Domain.Entities;
using IngSw_Application.DTOs;
namespace IngSw_Domain.Interfaces;

public interface IAuthRepository
{
    Task<User?> Login(UserDto.Request userData);
}
