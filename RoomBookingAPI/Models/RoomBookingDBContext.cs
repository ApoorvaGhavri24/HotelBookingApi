using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RoomBookingAPI.Models
{
    public partial class RoomBookingDBContext : DbContext
    {
      
        public RoomBookingDBContext(DbContextOptions<RoomBookingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Amenity> Amenity { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomAminity> RoomAminity { get; set; }
        public virtual DbSet<Status> Status { get; set; }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Amenity>(entity =>
            {
                entity.Property(e => e.IsActive).HasColumnName("Is_Active");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.CheckInDate).HasColumnType("date");

                entity.Property(e => e.CheckOutDate).HasColumnType("date");

                entity.Property(e => e.GuestFirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GuestLastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Booking__RoomId__4CA06362");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Booking__StatusI__4BAC3F29");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<RoomAminity>(entity =>
            {
                entity.Property(e => e.AmenityPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Amenity)
                    .WithMany(p => p.RoomAminity)
                    .HasForeignKey(d => d.AmenityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoomAmini__Ameni__60A75C0F");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomAminity)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RoomAmini__RoomI__619B8048");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.IsActive).HasColumnName("Is_Active");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
