using System.Security.Claims;
using Infrastructure.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MeController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public MeController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
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
}