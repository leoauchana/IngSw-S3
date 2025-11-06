using IngSw_Domain.Entities;
using IngSw_Infraestructure.Data.Connection;

namespace IngSw_Infraestructure.Data.DAOs;

public class AuthDao : DaoBase
{
    protected AuthDao(SqlConnection connection) : base(connection){ }
    public async Task<Dictionary<string, object>?> Login(User userData)
    {
        return null;
    }
}
