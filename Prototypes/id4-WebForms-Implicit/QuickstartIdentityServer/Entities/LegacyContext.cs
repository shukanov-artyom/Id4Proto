using System;
using Microsoft.EntityFrameworkCore;

namespace QuickstartIdentityServer.Entities
{
    public partial class LegacyContext : DbContext
    {
        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<UsersAndWebsite> UsersAndWebsite { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=orthobullets;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.UserId)
                    .HasName("_dta_index_Users_OB_892");

                entity.HasIndex(e => new { e.Firstname, e.Lastname, e.UserId })
                    .HasName("Users_UserID_INCLUDE_Firstname_Lastname_Index");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Fellowship)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GraduationYear)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Guid)
                    .HasColumnName("GUID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Internship).HasMaxLength(200);

                entity.Property(e => e.LandingPage)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MedicalSchool)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrivFellowship).HasColumnName("privFellowship");

                entity.Property(e => e.PrivFirstname).HasColumnName("privFirstname");

                entity.Property(e => e.PrivInternship).HasColumnName("privInternship");

                entity.Property(e => e.PrivLastname).HasColumnName("privLastname");

                entity.Property(e => e.PrivLocation).HasColumnName("privLocation");

                entity.Property(e => e.PrivPhoto).HasColumnName("privPhoto");

                entity.Property(e => e.PrivPosition).HasColumnName("privPosition");

                entity.Property(e => e.PrivResidency).HasColumnName("privResidency");

                entity.Property(e => e.PrivSchool).HasColumnName("privSchool");

                entity.Property(e => e.PrivSpecialty).HasColumnName("privSpecialty");

                entity.Property(e => e.PrivTitle).HasColumnName("privTitle");

                entity.Property(e => e.PrivUndergrad).HasColumnName("privUndergrad");

                entity.Property(e => e.ProfilePicture)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReferrerUrl)
                    .HasColumnName("ReferrerURL")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ReferrerUrlsession)
                    .HasColumnName("ReferrerURLSession")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Residency)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .HasColumnName("row_version")
                    .IsRowVersion();

                entity.Property(e => e.Specialty)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Undergrad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsernameDisplay)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsersAndWebsite>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.WebsiteId })
                    .HasName("UsersAndWebsite_Unq")
                    .IsUnique();

                entity.Property(e => e.ConfirmDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersAndWebsite)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UsersAndWebsite_Users");
            });
        }
    }
}
