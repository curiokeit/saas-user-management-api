using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasUserManagement.API.Data;

namespace SaasUserManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var tenantId = int.Parse(User.FindFirst("TenantId")!.Value);

            var tenant = await _context.Tenants
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == tenantId);

            if (tenant == null)
                return BadRequest("Tenant not found.");

            return Ok(new
            {
                tenant.Id,
                tenant.CompanyName,
                tenant.PlanName,
                tenant.UserLimit,
                CurrentUserCount = tenant.Users.Count,
                RemainingUserSlot = tenant.UserLimit - tenant.Users.Count
            });
        }
    }
}