using MarineIT.Domain.Entities;

namespace MarineIT.Application.Tickets
{
    public interface ITicketService
    {
        Task<Ticket> CreateAsync(CreateTicketDto dto, string reporterId);
        Task<Ticket?> GetAsync(int id);
        Task<IReadOnlyList<Ticket>> ListAsync(int page = 1, int pageSize = 20);
        Task<Ticket?> UpdateAsync(int id, UpdateTicketDto dto, string actorId);
        Task<bool> DeleteAsync(int id);
    }

    public record CreateTicketDto(string Title, string Category, int VesselId, int? AssetId, string Priority);
    public record UpdateTicketDto(string? Title, string? Category, int? AssetId, string? Priority, string? Status, DateTime? DueUtc);
}
