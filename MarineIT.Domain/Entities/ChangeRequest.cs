namespace MarineIT.Domain.Entities
{
    public class ChangeRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string RiskLevel { get; set; } = "Low";
        public DateTime PlannedStartUtc { get; set; }
        public DateTime PlannedEndUtc { get; set; }
        public int? PortWindowId { get; set; }
        public string Status { get; set; } = "Pending";
        public int OwnerId { get; set; }

        public PortWindow? PortWindow { get; set; }
        public ICollection<Approval> Approvals { get; set; } = new List<Approval>();
    }
}
