namespace MarineIT.Domain.Entities
{
    public class Approval
    {
        public int Id { get; set; }
        public int ChangeRequestId { get; set; }
        public string RoleRequired { get; set; } = string.Empty;
        public int? ApproverId { get; set; }
        public string Decision { get; set; } = "Pending";
        public DateTime? DecidedUtc { get; set; }

        public ChangeRequest ChangeRequest { get; set; } = null!;
    }
}
