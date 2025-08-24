using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Domain;

public class ApplicationUser : IdentityUser
{
    public ICollection<Festejo> Festejos { get; set; }
}