using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LayeringBookAPI.Models
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<LibStaff> LibStaffs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-GVHL84Q;Initial Catalog=Library;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Bid)
                    .HasName("PK__books__C6D111C957E7F470");

                entity.ToTable("books");

                entity.Property(e => e.Bid)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Author)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Bname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Jonour)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Bkid)
                    .HasName("PK__booking__38BD7D830D9A0D23");

                entity.ToTable("booking");

                entity.Property(e => e.Bkid).HasColumnName("BKid");

                entity.Property(e => e.Author)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Bid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("bid");

                entity.Property(e => e.Bname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Jonour)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("Total_Price");

                entity.HasOne(d => d.BidNavigation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.Bid)
                    .HasConstraintName("FK__booking__bid__02FC7413");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Cid)
                    .HasName("PK__client__C1FFD8618C8F73F7");

                entity.ToTable("client");

                entity.Property(e => e.Caddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAddress");

                entity.Property(e => e.Cname)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("Total_Price");

                entity.Property(e => e.Userid)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LibStaff>(entity =>
            {
                entity.HasKey(e => e.Sid)
                    .HasName("PK__LibStaff__CA1E5D788A1DF521");

                entity.ToTable("LibStaff");

                entity.Property(e => e.Lsname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LSname");

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Userid)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Client>().HasIndex(t => t.Userid).IsUnique();
            modelBuilder.Entity<Book>().HasIndex(t => t.Bname).IsUnique();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
