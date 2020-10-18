using Microsoft.EntityFrameworkCore;
using NewShoreTest.Models;
using System;

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

    public class Flights
    {
        public string Id { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime DepartureDate { get; set; }
        public TransportModel Transport { get; set; }
        public string FkTransporte { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}