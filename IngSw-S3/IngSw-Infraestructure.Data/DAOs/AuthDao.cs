using IngSw_Application.DTOs;
using IngSw_Infraestructure.Data.Connection;

namespace IngSw_Infraestructure.Data.DAOs;

public class AuthDao : DaoBase
{
    protected AuthDao(SqlConnection connection) : base(connection){ }
    public async Task<Dictionary<string, object>?> Login(UserDto.Request userData)
    {
        return null;
    }
}
