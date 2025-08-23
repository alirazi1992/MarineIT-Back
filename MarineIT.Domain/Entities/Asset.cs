namespace MarineIT.Domain.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public int VesselId { get; set; }
        public string Zone { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Serial { get; set; } = string.Empty;
        public string Firmware { get; set; } = string.Empty;
        public DateTime? LicenseExpiry { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? LastServiceUtc { get; set; }

        public Vessel Vessel { get; set; } = null!;
    }
}
