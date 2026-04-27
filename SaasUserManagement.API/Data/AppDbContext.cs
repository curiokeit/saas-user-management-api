using Microsoft.EntityFrameworkCore;
using SaasUserManagement.API.Models;

namespace SaasUserManagement.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<WorkTask> WorkTasks => Set<WorkTask>();
    }
}