using octo.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace octo.Infrastructure.Persistance
{
    public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<OctoUser>(options)
    {
        public DbSet<PersonalInformation> PersonalInformations { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<AccessPermission> AccessPermissions { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }

        // Akctive-related DbSets
        public DbSet<Akctivity> Activities { get; set; }
        public DbSet<HeartRate> HeartRates { get; set; }
        public DbSet<HealthMetric> HealthMetrics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OctoUser>()
                .HasOne(ld => ld.PersonalInformation)
                .WithOne(pi => pi.OctoBase)
                .HasForeignKey<PersonalInformation>(pi => pi.PersonalInformationId);

            modelBuilder.Entity<OctoUser>()
                .HasMany(ld => ld.Credentials)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<OctoUser>()
                .HasMany(ld => ld.ContactInformations)
                .WithOne(ci => ci.User)
                .HasForeignKey(ci => ci.UserId);

            modelBuilder.Entity<OctoUser>()
                .HasMany(ld => ld.AccessPermissions)
                .WithOne(ap => ap.User)
                .HasForeignKey(ap => ap.UserId);

            modelBuilder.Entity<OctoUser>()
                .HasMany(ld => ld.EmergencyContacts)
                .WithOne(ec => ec.User)
                .HasForeignKey(ec => ec.UserId);

            // Akctive relationships
            modelBuilder.Entity<OctoUser>()
                .HasMany(u => u.Activities)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<OctoUser>()
                .HasOne(u => u.HealthMetrics)
                .WithOne(hm => hm.User)
                .HasForeignKey<HealthMetric>(hm => hm.UserId);

            modelBuilder.Entity<Akctivity>()
                .HasKey(a => a.ActivityId);

            modelBuilder.Entity<HeartRate>()
                .HasKey(hr => hr.HeartRateId);

            modelBuilder.Entity<HealthMetric>()
                .HasKey(hm => hm.HealthMetricsId);

        }

        // Example method to load LoginDetails with related entities eagerly
        public OctoUser GetLoginDetailsWithAllData(string userId)
        {
            return this.Users
                .Include(ld => ld.PersonalInformation)
                .Include(ld => ld.Credentials)
                .Include(ld => ld.ContactInformations)
                .Include(ld => ld.AccessPermissions)
                .Include(ld => ld.EmergencyContacts)
                .Include(ld => ld.Activities)
                .Include(ld => ld.HealthMetrics)
                .SingleOrDefault(ld => ld.Id == userId);
        }
    }
}
