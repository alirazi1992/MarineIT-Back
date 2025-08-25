<<<<<<< HEAD
﻿using MarineIT.Domain.Entities;
using MarineIT.Infrastructure.Auth;
using MarineIT.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MarineIT.Infrastructure.Setup;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider sp)
    {
        using var scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var roles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var users = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        await db.Database.MigrateAsync();

        // Roles
        string[] roleNames = ["Admin", "FleetIT", "PortTech", "VesselRep", "NetSec", "OpsPlanner", "Captain", "Compliance"];
        foreach (var r in roleNames)
            if (!await roles.RoleExistsAsync(r))
                await roles.CreateAsync(new IdentityRole(r));

        // Admin
        if (await users.FindByNameAsync("admin") is null)
        {
            var u = new AppUser { UserName = "admin", Email = "admin@marine.local" };
            await users.CreateAsync(u, "Admin123$");
            await users.AddToRoleAsync(u, "Admin");
        }

        // Vessels
        if (!await db.Vessels.AnyAsync())
        {
            db.Vessels.AddRange(
                new Vessel { Name = "Ocean Explorer", IMO = "IMO1111111", Class = "Container", LastCommsQuality = "Operational" },
                new Vessel { Name = "Deep Surveyor", IMO = "IMO2222222", Class = "Research", LastCommsQuality = "Operational" },
                new Vessel { Name = "Marine Pioneer", IMO = "IMO3333333", Class = "Bulk", LastCommsQuality = "Maintenance" }
            );
            await db.SaveChangesAsync();
        }

        // A couple of tickets
        if (!await db.Tickets.AnyAsync())
        {
            var v1 = await db.Vessels.FirstAsync();
            db.Tickets.AddRange(
                new Ticket { Title = "Radar System Malfunction", Category = "Navigation", VesselId = v1.Id, Priority = "High", Status = "Open", CreatedUtc = DateTime.UtcNow },
                new Ticket { Title = "VHF Radio Antenna Check", Category = "Comms", VesselId = v1.Id, Priority = "Medium", Status = "InProgress", CreatedUtc = DateTime.UtcNow }
            );
            await db.SaveChangesAsync();
        }
    }
}
=======
﻿using MarineIT.Domain.Entities;
using MarineIT.Infrastructure.Auth;
using MarineIT.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MarineIT.Infrastructure.Setup;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider sp)
    {
        using var scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var roles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var users = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

        await db.Database.MigrateAsync();

        // Roles
        string[] roleNames = ["Admin", "FleetIT", "PortTech", "VesselRep", "NetSec", "OpsPlanner", "Captain", "Compliance"];
        foreach (var r in roleNames)
            if (!await roles.RoleExistsAsync(r))
                await roles.CreateAsync(new IdentityRole(r));

        // Admin
        if (await users.FindByNameAsync("admin") is null)
        {
            var u = new AppUser { UserName = "admin", Email = "admin@marine.local" };
            await users.CreateAsync(u, "Admin123$");
            await users.AddToRoleAsync(u, "Admin");
        }

        // Vessels
        if (!await db.Vessels.AnyAsync())
        {
            db.Vessels.AddRange(
                new Vessel { Name = "Ocean Explorer", IMO = "IMO1111111", Class = "Container", LastCommsQuality = "Operational" },
                new Vessel { Name = "Deep Surveyor", IMO = "IMO2222222", Class = "Research", LastCommsQuality = "Operational" },
                new Vessel { Name = "Marine Pioneer", IMO = "IMO3333333", Class = "Bulk", LastCommsQuality = "Maintenance" }
            );
            await db.SaveChangesAsync();
        }

        // A couple of tickets
        if (!await db.Tickets.AnyAsync())
        {
            var v1 = await db.Vessels.FirstAsync();
            db.Tickets.AddRange(
                new Ticket { Title = "Radar System Malfunction", Category = "Navigation", VesselId = v1.Id, Priority = "High", Status = "Open", CreatedUtc = DateTime.UtcNow },
                new Ticket { Title = "VHF Radio Antenna Check", Category = "Comms", VesselId = v1.Id, Priority = "Medium", Status = "InProgress", CreatedUtc = DateTime.UtcNow }
            );
            await db.SaveChangesAsync();
        }
    }
}
>>>>>>> 8dff6480899c1fea1e8f67768bb0ba1ed706e4e1
