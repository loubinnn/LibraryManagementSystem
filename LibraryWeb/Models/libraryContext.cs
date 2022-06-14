using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LibraryWeb.Models
{
    public partial class libraryContext : DbContext
    {
        public libraryContext()
        {
        }

        public libraryContext(DbContextOptions<libraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<LendingTicket> LendingTickets { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Sex> Sexes { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-P2K5PD6;Database=library;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.AuthorId)
                    .HasMaxLength(10)
                    .HasColumnName("AuthorID")
                    .IsFixedLength(true);

                entity.Property(e => e.AuthorName).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.BookId)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.AuthorId)
                    .HasMaxLength(10)
                    .HasColumnName("AuthorID")
                    .IsFixedLength(true);

                entity.Property(e => e.BookName).HasMaxLength(50);

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.NumberOfpage).HasColumnName("NumberOFPage");

                entity.Property(e => e.PubisherId)
                    .HasMaxLength(10)
                    .HasColumnName("PubisherID")
                    .IsFixedLength(true);

                entity.Property(e => e.PublishDate).HasColumnType("date");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Book_Author");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("FK_Book_Category");

                entity.HasOne(d => d.Pubisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PubisherId)
                    .HasConstraintName("FK_Book_Publisher");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Category1);

                entity.ToTable("Category");

                entity.Property(e => e.Category1)
                    .HasMaxLength(50)
                    .HasColumnName("Category");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<LendingTicket>(entity =>
            {
                entity.ToTable("LendingTicket");

                entity.Property(e => e.LendingTicketId)
                    .HasMaxLength(10)
                    .HasColumnName("LendingTicketID")
                    .IsFixedLength(true);

                entity.Property(e => e.BookId)
                    .HasMaxLength(10)
                    .HasColumnName("BookID")
                    .IsFixedLength(true);

                entity.Property(e => e.BorrowedDate).HasColumnType("date");

                entity.Property(e => e.ReturnedDate).HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .HasColumnName("StudentID")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.LendingTickets)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_LendingTicket_Book");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.LendingTickets)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_LendingTicket_Status");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.LendingTickets)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_LendingTicket_Student");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("Publisher");

                entity.Property(e => e.PublisherId)
                    .HasMaxLength(10)
                    .HasColumnName("PublisherID")
                    .IsFixedLength(true);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.PublisherName).HasMaxLength(50);
            });

            modelBuilder.Entity<Sex>(entity =>
            {
                entity.HasKey(e => e.Sex1);

                entity.ToTable("Sex");

                entity.Property(e => e.Sex1)
                    .HasMaxLength(10)
                    .HasColumnName("Sex")
                    .IsFixedLength(true);

                entity.Property(e => e.SexDetail)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.Status1);

                entity.ToTable("Status");

                entity.Property(e => e.Status1)
                    .HasMaxLength(10)
                    .HasColumnName("Status")
                    .IsFixedLength(true);

                entity.Property(e => e.StatusDetail)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .HasColumnName("StudentID")
                    .IsFixedLength(true);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Sex)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.StudentName).HasMaxLength(50);

                entity.HasOne(d => d.SexNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.Sex)
                    .HasConstraintName("FK_Student_Sex");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
