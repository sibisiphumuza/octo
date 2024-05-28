using octo.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509.SigI;

namespace octo.Infrastructure.Persistance
{
    public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<OctoUser>(options)
    {
        public DbSet<PersonalInformation> PersonalInformations { get; set; }
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }
        public DbSet<AccessPermission> AccessPermissions { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }

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
                .SingleOrDefault(ld => ld.Id == userId);
        }
    }
}
