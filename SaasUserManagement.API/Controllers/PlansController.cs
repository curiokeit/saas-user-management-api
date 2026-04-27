using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasUserManagement.API.Data;
using SaasUserManagement.API.Dtos;
using System.Security.Claims;

namespace SaasUserManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PlansController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlansController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetPlans()
        {
            var plans = new[]
            {
                new { PlanName = "Free", UserLimit = 3, MonthlyPrice = 0 },
                new { PlanName = "Pro", UserLimit = 10, MonthlyPrice = 19 },
                new { PlanName = "Business", UserLimit = 50, MonthlyPrice = 49 }
            };

            return Ok(plans);
        }

        [HttpPut("upgrade")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpgradePlan(UpdatePlanDto request)
        {
            var tenantId = int.Parse(User.FindFirst("TenantId")!.Value);

            var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);

            if (tenant == null)
                return BadRequest("Tenant not found.");

            switch (request.PlanName)
            {
                case "Free":
                    tenant.PlanName = "Free";
                    tenant.UserLimit = 3;
                    break;

                case "Pro":
                    tenant.PlanName = "Pro";
                    tenant.UserLimit = 10;
                    break;

                case "Business":
                    tenant.PlanName = "Business";
                    tenant.UserLimit = 50;
                    break;

                default:
                    return BadRequest("Invalid plan name.");
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Plan upgraded successfully.",
                tenant.Id,
                tenant.CompanyName,
                tenant.PlanName,
                tenant.UserLimit
            });
        }
    }
}