<<<<<<< HEAD
﻿namespace MarineIT.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; } = string.Empty;
        public int? VesselId { get; set; }

        public Vessel? Vessel { get; set; }
    }
}
=======
﻿namespace MarineIT.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Role { get; set; } = string.Empty;
        public int? VesselId { get; set; }

        public Vessel? Vessel { get; set; }
    }
}
>>>>>>> 8dff6480899c1fea1e8f67768bb0ba1ed706e4e1
