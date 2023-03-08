using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Contexts;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Employee>()
            .HasIndex(e => new
            {
                e.Email,
                e.PhoneNumber
            }).IsUnique();
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Profiling> Profilings { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<University> Universities { get; set; }
}
