<<<<<<< HEAD
﻿using MarineIT.Domain.Entities;

namespace MarineIT.Application.Vessels
{
    public interface IVesselService
    {
        Task<Vessel> CreateAsync(string name, string? imo);
        Task<IReadOnlyList<Vessel>> ListAsync();
    }
}
=======
﻿using MarineIT.Domain.Entities;

namespace MarineIT.Application.Vessels
{
    public interface IVesselService
    {
        Task<Vessel> CreateAsync(string name, string? imo);
        Task<IReadOnlyList<Vessel>> ListAsync();
    }
}
>>>>>>> 8dff6480899c1fea1e8f67768bb0ba1ed706e4e1
