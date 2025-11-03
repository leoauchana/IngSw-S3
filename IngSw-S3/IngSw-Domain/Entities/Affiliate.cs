using IngSw_Domain.Common;

namespace IngSw_Domain.Entities;

public class Affiliate : EntityBase
{
    public string? AffiliateNumber { get; set; }
    public SocialWork? SocialWork { get; set; }
}
