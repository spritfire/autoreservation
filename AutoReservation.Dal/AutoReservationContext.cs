using System.Configuration;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace AutoReservation.Dal
{
    public class AutoReservationContext
        : DbContext
    {
        public static readonly LoggerFactory LoggerFactory = new LoggerFactory(
            new[] { new ConsoleLoggerProvider((_, logLevel) => logLevel >= LogLevel.Information, true) }
        );

        public DbSet<Auto> Autos { get; set; }
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Reservation> Reservationen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .UseLoggerFactory(LoggerFactory) // Warning: Do not create a new ILoggerFactory instance each time
                    .UseSqlServer(ConfigurationManager.ConnectionStrings[nameof(AutoReservationContext)].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Reservation>()
                .HasKey(e => e.ReservationsNr);

            modelBuilder.Entity<Entities.Reservation>()
                .Property(e => e.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Entities.Kunde>()
                .Property(e => e.Nachname)
                .HasMaxLength(20);

            modelBuilder.Entity<Entities.Kunde>()
                .Property(e => e.Vorname)
                .HasMaxLength(20);

            modelBuilder.Entity<Entities.Kunde>()
                .Property(e => e.RowVersion)
                .IsRowVersion();
                        
            modelBuilder.Entity<Entities.Auto>()                
                .HasDiscriminator<int>("AutoKlasse")
                .HasValue<Entities.LuxusklasseAuto>(0)
                .HasValue<Entities.MittelklasseAuto>(1)
                .HasValue<Entities.StandardAuto>(2);

            modelBuilder.Entity<Entities.Auto>()
                .Property(e => e.Marke)
                .HasMaxLength(20);

            modelBuilder.Entity<Entities.Auto>()
                .Property(e => e.RowVersion)
                .IsRowVersion();
        }       
    }
}
