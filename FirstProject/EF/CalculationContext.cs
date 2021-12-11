using FirstProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.EF
{
    public class CalculationContext : DbContext
    {
        public CalculationContext(DbContextOptions<CalculationContext> opts) : base(opts) {}

        public DbSet<Calculation> Calculations { get; set; }
    }
}