using Audit.EntityFramework;
using Challenge.Core.Contracts.Repository;
using Challenge.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.Persistance.DBContext;

public sealed class RepositoryDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options)
       : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
    }

    public DbSet<Facts> Facts { get; set; }
    public DbSet<Breeds> Breeds { get; set; }

}
