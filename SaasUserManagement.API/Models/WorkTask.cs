namespace SaasUserManagement.API.Models
{
    public class WorkTask
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = "Todo";

        public int AssignedUserId { get; set; }

        public int TenantId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}