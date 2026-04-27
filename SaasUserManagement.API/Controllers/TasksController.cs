using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaasUserManagement.API.Data;
using SaasUserManagement.API.Dtos;
using SaasUserManagement.API.Models;

namespace SaasUserManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("my-tasks")]
        public async Task<IActionResult> GetMyTasks()
        {
            var tenantId = int.Parse(User.FindFirst("TenantId")!.Value);
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            var tasks = await _context.WorkTasks
                .Where(x => x.TenantId == tenantId && x.AssignedUserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(tasks);
        }


        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tenantId = int.Parse(User.FindFirst("TenantId")!.Value);
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)!.Value;

            var query = _context.WorkTasks
                .Where(x => x.TenantId == tenantId);

            if (role != "Admin")
            {
                query = query.Where(x => x.AssignedUserId == userId);
            }

            var tasks = await query
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTask(CreateTaskDto request)
        {
            var tenantId = int.Parse(User.FindFirst("TenantId")!.Value);

            var assignedUserExists = await _context.Users
                .AnyAsync(x => x.Id == request.AssignedUserId && x.TenantId == tenantId);

            if (!assignedUserExists)
                return BadRequest("Assigned user does not belong to your company.");

            var task = new WorkTask
            {
                Title = request.Title,
                Description = request.Description,
                AssignedUserId = request.AssignedUserId,
                TenantId = tenantId,
                Status = "Todo"
            };

            _context.WorkTasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Task created successfully.",
                task
            });
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateTaskStatusDto request)
        {
            var tenantId = int.Parse(User.FindFirst("TenantId")!.Value);

            var allowedStatuses = new[] { "Todo", "InProgress", "Done" };

            if (!allowedStatuses.Contains(request.Status))
                return BadRequest("Invalid status. Use: Todo, InProgress, Done.");

            var task = await _context.WorkTasks
                .FirstOrDefaultAsync(x => x.Id == id && x.TenantId == tenantId);

            if (task == null)
                return NotFound("Task not found.");

            task.Status = request.Status;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Task status updated successfully.",
                task
            });
        }
    }
}