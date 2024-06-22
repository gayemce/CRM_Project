using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // tables

        builder.Entity<Customer>(b =>
        {
            b.Property(m => m.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            b.Property(m => m.Name)
                .HasMaxLength(128);

            b.Property(m => m.Email)
                .HasMaxLength(64);

            b.Property(m => m.Phone)
                .HasMaxLength(32);

            b.Property(m => m.Address)
                .HasMaxLength(256);

            b.Property(m => m.CreateDate)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            b.Property(m => m.CreateDate)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnUpdate();

            b.HasMany(e => e.Contacts)
                .WithOne()
                .HasForeignKey("CustomerId")
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(e => e.Opportunities)
                .WithOne()
                .HasForeignKey("CustomerId")
                .OnDelete(DeleteBehavior.Cascade);

            b.HasMany(e => e.Activities)
                .WithOne()
                .HasForeignKey("CustomerId")
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
