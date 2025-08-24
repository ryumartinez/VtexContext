using System.Security.Claims;
using Infrastructure;
using Infrastructure.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FestejoController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public FestejoController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    // DTO/Model for creating a Festejo (replace or extend as needed)
    public class FestejoCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateFestejo([FromBody] FestejoCreateModel model)
    {
        // Get user ID from token claims
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        // Get the user entity from database
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Unauthorized();
        }

        // Create a new Festejo and assign the owner
        var festejo = new Festejo
        {
            Name = model.Name,
            Description = model.Description,
            Address = model.Address,
            DateFrom = model.DateFrom,
            DateTo = model.DateTo,
            Owners = new List<ApplicationUser> { user },
            Agasajados = new List<Agasajado>() // initialize if needed
        };

        _dbContext.Festejos.Add(festejo);
        await _dbContext.SaveChangesAsync();

        return Ok(festejo);
    }
}