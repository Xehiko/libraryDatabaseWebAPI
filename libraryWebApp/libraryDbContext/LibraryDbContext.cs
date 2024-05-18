using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using libraryWebApp.Models;

namespace libraryWebApp.libraryDbContext;

public partial class LibraryDbContext : DbContext
{
    public LibraryDbContext()
    {
    }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthorSak> AuthorSaks { get; set; }

    public virtual DbSet<BookSak> BookSaks { get; set; }

    public virtual DbSet<BookdepartmentSak> BookdepartmentSaks { get; set; }

    public virtual DbSet<BookingSak> BookingSaks { get; set; }

    public virtual DbSet<PublishinghouseSak> PublishinghouseSaks { get; set; }

    public virtual DbSet<ReaderSak> ReaderSaks { get; set; }

    public virtual DbSet<UserSak> UserSaks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorSak>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PRIMARY");
        });

        modelBuilder.Entity<BookSak>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PRIMARY");

            entity.HasOne(d => d.BookDepartment).WithMany(p => p.BookSaks).HasConstraintName("fk_Book_SAK_BookDepartment_SAK1");

            entity.HasOne(d => d.PublishingHouse).WithMany(p => p.BookSaks).HasConstraintName("fk_Book_SAK_PublishingHouse_SAK1");

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookhasauthorSak",
                    r => r.HasOne<AuthorSak>().WithMany()
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("fk_Book_SAK_has_Author_SAK_Author_SAK1"),
                    l => l.HasOne<BookSak>().WithMany()
                        .HasForeignKey("BookId")
                        .HasConstraintName("fk_Book_SAK_has_Author_SAK_Book_SAK1"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId").HasName("PRIMARY");
                        j.ToTable("bookhasauthor_sak");
                        j.HasIndex(new[] { "AuthorId" }, "fk_Book_SAK_has_Author_SAK_Author_SAK1_idx");
                        j.HasIndex(new[] { "BookId" }, "fk_Book_SAK_has_Author_SAK_Book_SAK1_idx");
                        j.IndexerProperty<int>("BookId").HasColumnName("BookID");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("AuthorID");
                    });
        });

        modelBuilder.Entity<BookdepartmentSak>(entity =>
        {
            entity.HasKey(e => e.DepId).HasName("PRIMARY");
        });

        modelBuilder.Entity<BookingSak>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PRIMARY");

            entity.HasOne(d => d.Book).WithMany(p => p.BookingSaks).HasConstraintName("BookID");

            entity.HasOne(d => d.Dep).WithMany(p => p.BookingSaks).HasConstraintName("DepID");

            entity.HasOne(d => d.Reader).WithMany(p => p.BookingSaks)
                .HasPrincipalKey(p => p.ReaderId)
                .HasForeignKey(d => d.ReaderId)
                .HasConstraintName("ReaderID");
        });

        modelBuilder.Entity<PublishinghouseSak>(entity =>
        {
            entity.HasKey(e => e.PublishingHouseId).HasName("PRIMARY");

            entity.HasMany(d => d.Authors).WithMany(p => p.PublishingHouses)
                .UsingEntity<Dictionary<string, object>>(
                    "PublishingSak",
                    r => r.HasOne<AuthorSak>().WithMany()
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("fk_PublishingHouse_SAK_has_Author_SAK_Author_SAK1"),
                    l => l.HasOne<PublishinghouseSak>().WithMany()
                        .HasForeignKey("PublishingHouseId")
                        .HasConstraintName("fk_PublishingHouse_SAK_has_Author_SAK_PublishingHouse_SAK"),
                    j =>
                    {
                        j.HasKey("PublishingHouseId", "AuthorId").HasName("PRIMARY");
                        j.ToTable("publishing_sak");
                        j.HasIndex(new[] { "AuthorId" }, "fk_PublishingHouse_SAK_has_Author_SAK_Author_SAK1_idx");
                        j.HasIndex(new[] { "PublishingHouseId" }, "fk_PublishingHouse_SAK_has_Author_SAK_PublishingHouse_SAK_idx");
                        j.IndexerProperty<int>("PublishingHouseId").HasColumnName("PublishingHouseID");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("AuthorID");
                    });
        });

        modelBuilder.Entity<ReaderSak>(entity =>
        {
            entity.HasKey(e => new { e.ReaderId, e.UserId }).HasName("PRIMARY");

            entity.Property(e => e.ReaderId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.User).WithMany(p => p.ReaderSaks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Reader_SAK_User1");
        });

        modelBuilder.Entity<UserSak>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
