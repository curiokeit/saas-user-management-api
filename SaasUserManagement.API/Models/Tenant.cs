namespace SaasUserManagement.API.Models
{
    public class Tenant
    {
        public int Id { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string PlanName { get; set; } = "Free";

        public int UserLimit { get; set; } = 3;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<User> Users { get; set; } = new();
    }
}