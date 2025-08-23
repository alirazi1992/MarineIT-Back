namespace MarineIT.Domain.Entities
{
    public class SLA
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ResponseMins { get; set; }
        public int ResolveMins { get; set; }
        public string AppliesToVesselClass { get; set; } = string.Empty;
    }
}
