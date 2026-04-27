using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasUserManagement.API.Data;
using SaasUserManagement.API.Dtos;
using SaasUserManagement.API.Models;
using System.Security.Claims;

namespace SaasUserManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var tenantId = int.Parse(User.FindFirst("TenantId")!.Value);

            var users = await _context.Users
                .Where(x => x.TenantId == tenantId)
                .Select(x => new
                {
                    x.Id,
                    x.FullName,
                    x.Email,
                    x.Role,
                    x.CreatedAt
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser(CreateUserDto request)
        {
            var tenantId = int.Parse(User.FindFirst("TenantId")!.Value);

            var tenant = await _context.Tenants
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == tenantId);

            if (tenant == null)
                return BadRequest("Tenant not found.");

            if (tenant.Users.Count >= tenant.UserLimit)
                return BadRequest("User limit reached. Please upgrade your plan.");

            var emailExists = await _context.Users.AnyAsync(x => x.Email == request.Email);

            if (emailExists)
                return BadRequest("Email already exists.");

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = "User",
                TenantId = tenantId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "User created successfully.",
                user = new
                {
                    user.Id,
                    user.FullName,
                    user.Email,
                    user.Role,
                    user.TenantId
                }
            });
        }
    }
}