namespace MarineIT.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int VesselId { get; set; }
        public int? AssetId { get; set; }
        public string Priority { get; set; } = "Normal";
        public string Status { get; set; } = "Open";
        public int? SLAId { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public DateTime? DueUtc { get; set; }
        public DateTime? ResolvedUtc { get; set; }
        public int ReporterId { get; set; }
        public int? AssigneeId { get; set; }

        public Vessel Vessel { get; set; } = null!;
        public Asset? Asset { get; set; }
    }
}
