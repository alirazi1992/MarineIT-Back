namespace MarineIT.Domain.Entities
{
    public class Attachment
    {
        public int Id { get; set; }
        public int? TicketId { get; set; }
        public int? ChangeRequestId { get; set; }
        public string Url { get; set; } = string.Empty;
        public int SizeKB { get; set; }

        public Ticket? Ticket { get; set; }
        public ChangeRequest? ChangeRequest { get; set; }
    }
}
