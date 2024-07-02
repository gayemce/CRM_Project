using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;
public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Customer>(b =>
        {
            b.Property(m => m.Id)
            .HasDefaultValueSql("NEWID()");

            b.Property(m => m.Name)
                .HasMaxLength(128);

            b.Property(m => m.Email)
                .HasMaxLength(64);

            b.Property(m => m.Phone)
                .HasMaxLength(32);

            b.Property(m => m.Address)
                .HasMaxLength(256);

            b.Property(m => m.CreateDate)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            b.Property(m => m.UpdateDate)
                .HasDefaultValueSql("GETDATE()")
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

        builder.Entity<Product>(b =>
        {
            b.Property(m => m.Id)
            .HasDefaultValueSql("NEWID()");

            b.Property(m => m.Name)
                .HasMaxLength(128);

            b.Property(m => m.Description)
                .HasMaxLength(256);

            b.Property(m => m.CreateDate)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            b.Property(m => m.UpdateDate)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnUpdate();
        });

        builder.Ignore<Activity>();
        builder.Ignore<Contact>();
        builder.Ignore<Lead>();
        builder.Ignore<Opportunity>();

        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityUserRole<Guid>>();
    }
}
