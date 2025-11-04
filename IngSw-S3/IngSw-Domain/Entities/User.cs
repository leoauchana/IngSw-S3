using IngSw_Domain.Common;

namespace IngSw_Domain.Entities;

public class User : EntityBase
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
