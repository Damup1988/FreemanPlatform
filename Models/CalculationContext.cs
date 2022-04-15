using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Platform.Models
{
    public class CalculationContext : DbContext
    {
        public DbSet<Calculation> Calculations { get; set; }
        
        public CalculationContext(DbContextOptions<CalculationContext> opts) : base(opts)
        {
            
        }
    }
}