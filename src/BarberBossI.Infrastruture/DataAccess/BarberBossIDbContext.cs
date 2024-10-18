using BarberBossI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBossI.Infrastruture.DataAccess;
internal class BarberBossIDbContext : DbContext
{
    public BarberBossIDbContext(DbContextOptions options) : base(options) {  }
    public DbSet<Invoice> Invoices { get; set; }

   
}
