using System.Security.Claims;
using Infrastructure;
using Infrastructure.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MeController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;

    public MeController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    public class MeDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        // Add other user properties you want to expose safely
    }

    public class UpdateMeModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        // Add properties you want user to update
    }

    [HttpGet]
    public async Task<IActionResult> GetMyProfile()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        var meDto = new MeDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };

        return Ok(meDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateMeModel model)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        user.UserName = model.UserName ?? user.UserName;
        user.Email = model.Email ?? user.Email;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return NoContent();
    }
    
     [HttpPost("request-ownership/{festejoId}")]
    public async Task<IActionResult> RequestOwnership(int festejoId)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var festejo = await _dbContext.Festejos.FindAsync(festejoId);
        if (festejo == null)
            return NotFound("Festejo not found");

        // Check if an existing request exists
        var existingRequest = await _dbContext.OwnershipRequests
            .FirstOrDefaultAsync(r => r.UserId == userId && r.FestejoId == festejoId && !r.IsApproved);
        if (existingRequest != null)
            return BadRequest("You have already requested ownership for this Festejo.");

        var ownershipRequest = new OwnershipRequest
        {
            UserId = userId,
            FestejoId = festejoId,
            IsApproved = false
        };

        _dbContext.OwnershipRequests.Add(ownershipRequest);
        await _dbContext.SaveChangesAsync();

        return Ok("Ownership request submitted.");
    }
    
    [HttpPost("accept-ownership-request/{requestId}")]
    public async Task<IActionResult> AcceptOwnershipRequest(int requestId)
    {
        var request = await _dbContext.OwnershipRequests
            .Include(r => r.User)
            .Include(r => r.Festejo).ThenInclude(festejo => festejo.Owners)
            .FirstOrDefaultAsync(r => r.Id == requestId);

        if (request == null)
            return NotFound("Ownership request not found");

        if (request.IsApproved)
            return BadRequest("Request has already been approved.");

        // Add user as owner if not already owner
        if (request.Festejo.Owners == null)
            request.Festejo.Owners = new List<ApplicationUser>();

        if (!request.Festejo.Owners.Contains(request.User))
            request.Festejo.Owners.Add(request.User);

        request.IsApproved = true;

        await _dbContext.SaveChangesAsync();

        return Ok("Ownership request approved, user added as owner.");
    }
    
    [HttpPost("festejo/{festejoId}/add-owner/{newOwnerId}")]
    public async Task<IActionResult> AddOwner(int festejoId, string newOwnerId)
    {
        string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(currentUserId))
            return Unauthorized();

        var festejo = await _dbContext.Festejos
            .Include(f => f.Owners)
            .FirstOrDefaultAsync(f => f.Id == festejoId);

        if (festejo == null)
            return NotFound("Festejo not found");

        // Check if current user is an owner of the Festejo
        if (festejo.Owners == null || !festejo.Owners.Any(o => o.Id == currentUserId))
            return Forbid("You are not an owner of this Festejo");

        var newOwner = await _dbContext.Users.FindAsync(newOwnerId);
        if (newOwner == null)
            return NotFound("User to add as owner not found");

        // Initialize Owners collection if null
        if (festejo.Owners == null)
            festejo.Owners = new List<ApplicationUser>();

        if (festejo.Owners.Any(o => o.Id == newOwnerId))
            return BadRequest("User is already an owner");

        festejo.Owners.Add(newOwner);
        await _dbContext.SaveChangesAsync();

        return Ok("User added as owner successfully");
    }
    
    [HttpPost("festejo/{festejoId}/remove-owner")]
    public async Task<IActionResult> RemoveSelfAsOwner(int festejoId)
    {
        string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(currentUserId))
            return Unauthorized();

        var festejo = await _dbContext.Festejos
            .Include(f => f.Owners)
            .FirstOrDefaultAsync(f => f.Id == festejoId);

        if (festejo == null)
            return NotFound("Festejo not found");

        if (festejo.Owners == null || !festejo.Owners.Any(o => o.Id == currentUserId))
            return BadRequest("You are not an owner of this Festejo");

        var ownerToRemove = festejo.Owners.FirstOrDefault(o => o.Id == currentUserId);
        if (ownerToRemove != null)
        {
            festejo.Owners.Remove(ownerToRemove);
            await _dbContext.SaveChangesAsync();
            return Ok("You are no longer an owner of this Festejo");
        }

        return BadRequest("Failed to remove ownership");
    }

}