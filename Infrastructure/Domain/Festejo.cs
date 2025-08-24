namespace Infrastructure.Domain;

public class Festejo
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
    
    public string Address { get; set; }
    
    public ICollection<Agasajado> Agasajados { get; set; }
    public ICollection<User> Owners { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }
}