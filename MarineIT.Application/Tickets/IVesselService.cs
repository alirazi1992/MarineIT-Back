using MarineIT.Domain.Entities;

namespace MarineIT.Application.Vessels
{
    public interface IVesselService
    {
        Task<Vessel> CreateAsync(string name, string? imo);
        Task<IReadOnlyList<Vessel>> ListAsync();
    }
}
