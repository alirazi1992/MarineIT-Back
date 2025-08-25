<<<<<<< HEAD
﻿using MarineIT.Application.Tickets;
using MarineIT.Domain.Entities;
using MarineIT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MarineIT.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _db;
        public TicketService(AppDbContext db) => _db = db;

        public async Task<Ticket> CreateAsync(CreateTicketDto dto, string reporterId)
        {
            var t = new Ticket
            {
                Title = dto.Title,
                Category = dto.Category,
                VesselId = dto.VesselId,
                AssetId = dto.AssetId,
                Priority = dto.Priority,
                Status = "Open",
                CreatedUtc = DateTime.UtcNow,
                ReporterId = 0
            };
            _db.Tickets.Add(t);
            _db.TicketEvents.Add(new TicketEvent
            {
                Ticket = t,
                Type = "Created",
                Notes = "Ticket created",
                ActorId = 0,
                WhenUtc = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();
            return t;
        }

        public Task<Ticket?> GetAsync(int id) =>
            _db.Tickets.Include(x => x.Vessel).Include(x => x.Asset).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IReadOnlyList<Ticket>> ListAsync(int page = 1, int pageSize = 20) =>
            await _db.Tickets.AsNoTracking()
                .OrderByDescending(t => t.CreatedUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public async Task<Ticket?> UpdateAsync(int id, UpdateTicketDto dto, string actorId)
        {
            var t = await _db.Tickets.FindAsync(id);
            if (t is null) return null;

            if (dto.Title != null) t.Title = dto.Title;
            if (dto.Category != null) t.Category = dto.Category;
            if (dto.AssetId.HasValue) t.AssetId = dto.AssetId.Value;
            if (dto.Priority != null) t.Priority = dto.Priority;
            if (dto.Status != null)
            {
                t.Status = dto.Status;
                if (dto.Status == "Resolved") t.ResolvedUtc = DateTime.UtcNow;
            }
            if (dto.DueUtc.HasValue) t.DueUtc = dto.DueUtc;

            _db.TicketEvents.Add(new TicketEvent
            {
                TicketId = t.Id,
                Type = "Updated",
                Notes = "Ticket updated",
                ActorId = 0,
                WhenUtc = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();
            return t;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var t = await _db.Tickets.FindAsync(id);
            if (t is null) return false;
            _db.Tickets.Remove(t);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
=======
﻿using MarineIT.Application.Tickets;
using MarineIT.Domain.Entities;
using MarineIT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MarineIT.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _db;
        public TicketService(AppDbContext db) => _db = db;

        public async Task<Ticket> CreateAsync(CreateTicketDto dto, string reporterId)
        {
            var t = new Ticket
            {
                Title = dto.Title,
                Category = dto.Category,
                VesselId = dto.VesselId,
                AssetId = dto.AssetId,
                Priority = dto.Priority,
                Status = "Open",
                CreatedUtc = DateTime.UtcNow,
                ReporterId = 0
            };
            _db.Tickets.Add(t);
            _db.TicketEvents.Add(new TicketEvent
            {
                Ticket = t,
                Type = "Created",
                Notes = "Ticket created",
                ActorId = 0,
                WhenUtc = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();
            return t;
        }

        public Task<Ticket?> GetAsync(int id) =>
            _db.Tickets.Include(x => x.Vessel).Include(x => x.Asset).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IReadOnlyList<Ticket>> ListAsync(int page = 1, int pageSize = 20) =>
            await _db.Tickets.AsNoTracking()
                .OrderByDescending(t => t.CreatedUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public async Task<Ticket?> UpdateAsync(int id, UpdateTicketDto dto, string actorId)
        {
            var t = await _db.Tickets.FindAsync(id);
            if (t is null) return null;

            if (dto.Title != null) t.Title = dto.Title;
            if (dto.Category != null) t.Category = dto.Category;
            if (dto.AssetId.HasValue) t.AssetId = dto.AssetId.Value;
            if (dto.Priority != null) t.Priority = dto.Priority;
            if (dto.Status != null)
            {
                t.Status = dto.Status;
                if (dto.Status == "Resolved") t.ResolvedUtc = DateTime.UtcNow;
            }
            if (dto.DueUtc.HasValue) t.DueUtc = dto.DueUtc;

            _db.TicketEvents.Add(new TicketEvent
            {
                TicketId = t.Id,
                Type = "Updated",
                Notes = "Ticket updated",
                ActorId = 0,
                WhenUtc = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();
            return t;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var t = await _db.Tickets.FindAsync(id);
            if (t is null) return false;
            _db.Tickets.Remove(t);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
>>>>>>> 8dff6480899c1fea1e8f67768bb0ba1ed706e4e1
