<<<<<<< HEAD
﻿namespace MarineIT.Domain.Entities
{
    public class TicketEvent
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int ActorId { get; set; }
        public DateTime WhenUtc { get; set; } = DateTime.UtcNow;

        public Ticket Ticket { get; set; } = null!;
    }
}
=======
﻿namespace MarineIT.Domain.Entities
{
    public class TicketEvent
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int ActorId { get; set; }
        public DateTime WhenUtc { get; set; } = DateTime.UtcNow;

        public Ticket Ticket { get; set; } = null!;
    }
}
>>>>>>> 8dff6480899c1fea1e8f67768bb0ba1ed706e4e1
