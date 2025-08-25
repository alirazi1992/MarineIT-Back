<<<<<<< HEAD
﻿using System.Net.Sockets;

namespace MarineIT.Domain.Entities
{
    public class Vessel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IMO { get; set; } = string.Empty;
        public string Fleet { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string? LastKnownPosition { get; set; }
        public string? LastCommsQuality { get; set; }

        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<PortWindow> PortWindows { get; set; } = new List<PortWindow>();
    }
}
=======
﻿using System.Net.Sockets;

namespace MarineIT.Domain.Entities
{
    public class Vessel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IMO { get; set; } = string.Empty;
        public string Fleet { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string? LastKnownPosition { get; set; }
        public string? LastCommsQuality { get; set; }

        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<PortWindow> PortWindows { get; set; } = new List<PortWindow>();
    }
}
>>>>>>> 8dff6480899c1fea1e8f67768bb0ba1ed706e4e1
