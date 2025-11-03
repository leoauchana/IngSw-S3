namespace IngSw_Domain.Entities;

public class Patient : Person
{
    public Affiliate? SocialWork { get; set; }
    public Domicilie? Domicilie { get; set; }
}
