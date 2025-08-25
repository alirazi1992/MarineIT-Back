<<<<<<< HEAD
﻿using MarineIT.Application.Vessels;
using MarineIT.Domain.Entities;
using MarineIT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MarineIT.Infrastructure.Services
{
    public class VesselService : IVesselService
    {
        private readonly AppDbContext _db;
        public VesselService(AppDbContext db) => _db = db;

        public async Task<Vessel> CreateAsync(string name, string? imo)
        {
            var v = new Vessel { Name = name, IMO = imo ?? string.Empty };
            _db.Vessels.Add(v);
            await _db.SaveChangesAsync();
            return v;
        }

        public async Task<IReadOnlyList<Vessel>> ListAsync() =>
            await _db.Vessels.AsNoTracking().OrderBy(v => v.Name).ToListAsync();
    }
}
=======
﻿using MarineIT.Application.Vessels;
using MarineIT.Domain.Entities;
using MarineIT.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MarineIT.Infrastructure.Services
{
    public class VesselService : IVesselService
    {
        private readonly AppDbContext _db;
        public VesselService(AppDbContext db) => _db = db;

        public async Task<Vessel> CreateAsync(string name, string? imo)
        {
            var v = new Vessel { Name = name, IMO = imo ?? string.Empty };
            _db.Vessels.Add(v);
            await _db.SaveChangesAsync();
            return v;
        }

        public async Task<IReadOnlyList<Vessel>> ListAsync() =>
            await _db.Vessels.AsNoTracking().OrderBy(v => v.Name).ToListAsync();
    }
}
>>>>>>> 8dff6480899c1fea1e8f67768bb0ba1ed706e4e1
