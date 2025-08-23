using MarineIT.Domain.Entities;
using MarineIT.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarineIT.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Vessel> Vessels => Set<Vessel>();
        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<TicketEvent> TicketEvents => Set<TicketEvent>();
        public DbSet<SLA> SLAs => Set<SLA>();
        public DbSet<KnowledgeArticle> KnowledgeArticles => Set<KnowledgeArticle>();
        public DbSet<PortWindow> PortWindows => Set<PortWindow>();
        public DbSet<ChangeRequest> ChangeRequests => Set<ChangeRequest>();
        public DbSet<Approval> Approvals => Set<Approval>();
        public DbSet<Attachment> Attachments => Set<Attachment>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            // Vessel relationships — avoid multiple cascade paths
            b.Entity<Asset>()
                .HasOne(a => a.Vessel)
                .WithMany(v => v.Assets)
                .HasForeignKey(a => a.VesselId)
                .OnDelete(DeleteBehavior.NoAction);   // safest: NoAction

            b.Entity<Ticket>()
                .HasOne(t => t.Vessel)
                .WithMany(v => v.Tickets)
                .HasForeignKey(t => t.VesselId)
                .OnDelete(DeleteBehavior.NoAction);   // <-- was Cascade; change to NoAction

            b.Entity<PortWindow>()
                .HasOne(p => p.Vessel)
                .WithMany(v => v.PortWindows)
                .HasForeignKey(p => p.VesselId)
                .OnDelete(DeleteBehavior.NoAction);   // <-- NoAction

            // Other relationships
            b.Entity<Ticket>()
                .HasOne(t => t.Asset)
                .WithMany()
                .HasForeignKey(t => t.AssetId)
                .OnDelete(DeleteBehavior.SetNull);

            b.Entity<TicketEvent>()
                .HasOne(e => e.Ticket)
                .WithMany()
                .HasForeignKey(e => e.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Entity<ChangeRequest>()
                .HasOne(c => c.PortWindow)
                .WithMany(p => p.ChangeRequests)
                .HasForeignKey(c => c.PortWindowId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
