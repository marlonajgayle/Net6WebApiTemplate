using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Domain.Common;
using Net6WebApiTemplate.Domain.Entities;
using Net6WebApiTemplate.Infrastructure.Identity;

namespace Net6WebApiTemplate.Persistence
{
    public class Net6WebApiTemplateDbContext : IdentityDbContext<ApplicationUser>, INet6WebApiTemplateDbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public Net6WebApiTemplateDbContext(DbContextOptions<Net6WebApiTemplateDbContext> options)
            : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = DateTime.UtcNow;
                        break;
                }

            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Net6WebApiTemplateDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}