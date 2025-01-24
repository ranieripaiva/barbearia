using BarberBossI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBossI.Infrastruture.DataAccess;
public class BarberBossIDbContext : DbContext
{
    public BarberBossIDbContext(DbContextOptions options) : base(options) {  }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tag>().ToTable("Tags");
    }

}
