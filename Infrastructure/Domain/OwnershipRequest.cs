namespace Infrastructure.Domain;

public class OwnershipRequest
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public int FestejoId { get; set; }
    public Festejo Festejo { get; set; }
    public bool IsApproved { get; set; } = false;
}