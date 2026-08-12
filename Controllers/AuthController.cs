using CampusEats.Api.Data; 
using CampusEats.Api.Dtos; 
using CampusEats.Api.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
  
namespace CampusEats.Api.Controllers; 
  
[ApiController] 
[Route("api/[controller]")]            // → /api/auth 
public class AuthController : ControllerBase 
{ 
    private readonly AppDbContext _db; 
    private readonly IConfiguration _cfg; 
    public AuthController(AppDbContext db, 
                          IConfiguration cfg) 
    { _db = db; _cfg = cfg; } 
  
    [HttpPost("register")] 
    public async Task<IActionResult> Register( 
        RegisterDto dto) 
    { 
        var taken = await _db.Users 
            .AnyAsync(u => u.Email == dto.Email); 
        if (taken) return Conflict("Email already used"); 
  
        var user = new User 
        { 
            Name = dto.Name, 
            Email = dto.Email, 

PasswordHash = 
BCrypt.Net.BCrypt.HashPassword(dto.Password),  // hash! 
Role = "Customer" 
}; 
_db.Users.Add(user); 
await _db.SaveChangesAsync(); 
return StatusCode(201, new
     { user.Id, user.Email, user.Role });

}
} 

