namespace MarineIT.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; } = string.Empty;
        public int? VesselId { get; set; }

        public Vessel? Vessel { get; set; }
    }
}
