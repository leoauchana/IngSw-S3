using IngSw_Domain.Common;
using IngSw_Domain.ValueObjects;

namespace IngSw_Domain.Entities;

public class Person : EntityBase
{
    public Cuil? Cuil { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}
