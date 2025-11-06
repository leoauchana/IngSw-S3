using IngSw_Domain.Entities;
namespace IngSw_Domain.Interfaces;

public interface IAuthRepository
{
    Task<User?> Login(User userData);
}
