using IngSw_Application.DTOs;
using IngSw_Domain.Entities;
using IngSw_Domain.Interfaces;
using IngSw_Infraestructure.Data.DAOs;

namespace IngSw_Infraestructure.Data.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly AuthDao _authDao;

    public AuthRepository(AuthDao authDao)
    {
        _authDao = authDao;
    }
    public async Task<User?> Login(UserDto.Request userData)
    {
        var userFound = _authDao.Login(userData);
        return null;
    }

    public Task<User?> Login(User userData)
    {
        var userFound = _authDao.Login(userData);
        return null;
    }


    //private Nurse MapEntity(Dictionary<string, object>? reader) where TEntity : EntityBase
    //{
    //    return new Patient
    //    {
    //        Id = (Guid)reader["id"],
    //        Name = reader["name"]?.ToString(),
    //        LastName = reader["last_name"].ToString(),
    //        Cuil = Cuil.Create(reader["cuil"].ToString()),
    //        Domicilie = new Domicilie
    //        {
    //            Number = Convert.ToInt32(reader["number"]),
    //            Street = reader["street"].ToString(),
    //            Locality = reader["locality"].ToString()
    //        }
    //    };
    //}
}
