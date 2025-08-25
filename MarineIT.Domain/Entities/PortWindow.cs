<<<<<<< HEAD
﻿namespace MarineIT.Domain.Entities
{
    public class PortWindow
    {
        public int Id { get; set; }
        public int VesselId { get; set; }
        public string PortName { get; set; } = string.Empty;
        public DateTime ETAUtc { get; set; }
        public DateTime ETDUtc { get; set; }
        public string? Notes { get; set; }

        public Vessel Vessel { get; set; } = null!;
        public ICollection<ChangeRequest> ChangeRequests { get; set; } = new List<ChangeRequest>();
    }
}
=======
﻿namespace MarineIT.Domain.Entities
{
    public class PortWindow
    {
        public int Id { get; set; }
        public int VesselId { get; set; }
        public string PortName { get; set; } = string.Empty;
        public DateTime ETAUtc { get; set; }
        public DateTime ETDUtc { get; set; }
        public string? Notes { get; set; }

        public Vessel Vessel { get; set; } = null!;
        public ICollection<ChangeRequest> ChangeRequests { get; set; } = new List<ChangeRequest>();
    }
}
>>>>>>> 8dff6480899c1fea1e8f67768bb0ba1ed706e4e1
