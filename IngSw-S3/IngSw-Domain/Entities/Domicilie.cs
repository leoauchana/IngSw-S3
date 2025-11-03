using IngSw_Domain.Common;

namespace IngSw_Domain.Entities;

public class Domicilie : EntityBase
{
    public int Number { get; set; }
    public string? Street { get; set; }
    public string? Locality { get; set; }
}
