using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AtletiAPI.Models
{
    public partial class OlympicsContext : DbContext
    {
        public OlympicsContext()
        {
        }

        public OlympicsContext(DbContextOptions<OlympicsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Athlete> Athletes { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Sport> Sports { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-LL4DG0N\\SQLEXPRESSNEW;Database=Olympics;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Athlete>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Bronze).HasColumnName("bronze");

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.FkNationality).HasColumnName("fk_Nationality");

                entity.Property(e => e.FkSport).HasColumnName("FK_Sport");

                entity.Property(e => e.Gold).HasColumnName("gold");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Silver).HasColumnName("silver");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Athletes)
                    .HasForeignKey(d => d.FkNationality)
                    .HasConstraintName("FK_Athlets_Nationalities");

                entity.HasOne(d => d.Sport)
                    .WithMany(p => p.Athletes)
                    .HasForeignKey(d => d.FkSport)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Athlets_Sports");
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
