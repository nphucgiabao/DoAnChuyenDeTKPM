using System;
using booking_api.Infrastructure.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace booking_api.Infrastructure.Repository
{
    public partial class car_bookingContext : DbContext
    {
        public car_bookingContext()
        {
        }

        public car_bookingContext(DbContextOptions<car_bookingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<BookingHistory> BookingHistory { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<TypeCar> TypeCar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-RI384NB;Database=car_booking;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DiemDen).IsRequired();

                entity.Property(e => e.DiemDon).IsRequired();

                entity.Property(e => e.UnitPrice).IsRequired();

                entity.Property(e => e.DriverId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BookingHistory>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Avartar).IsUnicode(false);

                entity.Property(e => e.BienSoXe)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TypeCar>(entity =>
            {
                entity.Property(e => e.GiaCuoc2Kmdau)
                    .HasColumnName("GiaCuoc2KMDau")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.GiaCuocSau2Km)
                    .HasColumnName("GiaCuocSau2KM")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
