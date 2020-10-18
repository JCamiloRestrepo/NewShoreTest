using Microsoft.EntityFrameworkCore;
using NewShoreTest.Models;

namespace NewShoreTest.Context
{
    public class NewShoreContext : DbContext
    {
        public DbSet<FlightModel> Flights { get; set; }
        public DbSet<TransportModel> Transports { get; set; }

        public NewShoreContext(DbContextOptions<NewShoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FlightModel>()
                .HasIndex(t => t.FkTransporte)
                .IsUnique();
            modelBuilder.Entity<TransportModel>();
        }
    }
}