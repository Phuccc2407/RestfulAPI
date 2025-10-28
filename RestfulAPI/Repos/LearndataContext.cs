using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestfulAPI.Repos.Models;

namespace RestfulAPI.Repos;

public partial class LearndataContext : IdentityDbContext<User>
{
    public LearndataContext()
    {
    }

    public LearndataContext(DbContextOptions<LearndataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<Follow> Follows { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<PlaylistItem> PlaylistItems { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<StreamHistory> StreamHistories { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<TrackLike> TrackLikes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=DESKTOP-ANVF8SQ\\SQLEXPRESS;Database=NhacNheoDB;Trusted_Connection=True;TrustServerCertificate=True;",
            sqlOptions =>
            {
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("PK__Albums__97B4BE37BF04CD91");

            entity.Property(e => e.AlbumId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Albums__ArtistId__66603565");

            entity.HasOne(d => d.Label).WithMany(p => p.Albums).HasConstraintName("FK__Albums__LabelId__6754599E");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__Artists__25706B50ADD40E33");

            entity.Property(e => e.ArtistId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.User).WithMany(p => p.Artists).HasConstraintName("FK__Artists__UserId__5BE2A6F2");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__AuditLog__A17F2398F5B3673B");

            entity.Property(e => e.PerformedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.PerformedByNavigation).WithMany(p => p.AuditLogs).HasConstraintName("FK__AuditLogs__Perfo__17036CC0");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Genres__0385057E0B602F56");
        });

        modelBuilder.Entity<Follow>(entity =>
        {
            entity.HasKey(e => e.FollowId).HasName("PK__Follows__2CE810AE40DC9507");

            entity.Property(e => e.FollowedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.FolloweeArtist).WithMany(p => p.Follows).HasConstraintName("FK__Follows__Followe__05D8E0BE");

            entity.HasOne(d => d.FolloweeUser).WithMany(p => p.FollowFolloweeUsers).HasConstraintName("FK__Follows__Followe__06CD04F7");

            entity.HasOne(d => d.Follower).WithMany(p => p.FollowFollowers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Follows__Followe__04E4BC85");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385057E0B602F56");
        });

        modelBuilder.Entity<Label>(entity =>
        {
            entity.HasKey(e => e.LabelId).HasName("PK__Labels__397E2BC3B9882E42");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A38727CDAFC");

            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__UserId__1332DBDC");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.PlaylistId).HasName("PK__Playlist__B30167A0FD82F77C");

            entity.Property(e => e.PlaylistId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsPublic).HasDefaultValue(true);

            entity.HasOne(d => d.OwnerUser).WithMany(p => p.Playlists)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Playlists__Owner__778AC167");
        });

        modelBuilder.Entity<PlaylistItem>(entity =>
        {
            entity.HasKey(e => e.PlaylistItemId).HasName("PK__Playlist__1910CEADF01446AF");

            entity.Property(e => e.AddedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Playlist).WithMany(p => p.PlaylistItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlaylistI__Playl__7B5B524B");

            entity.HasOne(d => d.Track).WithMany(p => p.PlaylistItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PlaylistI__Track__7C4F7684");
        });

        modelBuilder.Entity<StreamHistory>(entity =>
        {
            entity.HasKey(e => e.StreamId).HasName("PK__StreamHi__07C87A929CDEFD37");

            entity.Property(e => e.StartedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Track).WithMany(p => p.StreamHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StreamHis__Track__0A9D95DB");

            entity.HasOne(d => d.User).WithMany(p => p.StreamHistories).HasConstraintName("FK__StreamHis__UserI__0B91BA14");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__9A2B249D334785FF");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subscript__UserI__0F624AF8");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.TrackId).HasName("PK__Tracks__7A74F8E05D1F6F84");

            entity.Property(e => e.TrackId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Explicit).HasDefaultValue(false);

            // Quan hệ Track -> Album (1-n)
            entity.HasOne(d => d.Album)
                .WithMany(p => p.Tracks)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK__Tracks__AlbumId__6D0D32F4");

            // Quan hệ Track <-> Artist nhiều-nhiều qua TrackArtist
            modelBuilder.Entity<TrackArtist>(ta =>
            {
                ta.HasKey(x => new { x.TrackId, x.ArtistId });

                ta.HasOne(x => x.Track)
                    .WithMany(t => t.TrackArtists)
                    .HasForeignKey(x => x.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                ta.HasOne(x => x.Artist)
                    .WithMany(a => a.TrackArtists)
                    .HasForeignKey(x => x.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                ta.ToTable("TrackArtists");
            });

            // Quan hệ Track <-> Genre nhiều-nhiều qua TrackGenre
            modelBuilder.Entity<TrackGenre>(tg =>
            {
                tg.HasKey(x => new { x.TrackId, x.GenreId });

                tg.HasOne(x => x.Track)
                    .WithMany(t => t.TrackGenres)
                    .HasForeignKey(x => x.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                tg.HasOne(x => x.Genre)
                    .WithMany(g => g.TrackGenres)
                    .HasForeignKey(x => x.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                tg.ToTable("TrackGenres");
            });

            modelBuilder.Entity<TrackCategory>(tg =>
            {
                tg.HasKey(x => new { x.TrackId, x.CategoryId });

                tg.HasOne(x => x.Track)
                    .WithMany(t => t.TrackCategories)
                    .HasForeignKey(x => x.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                tg.HasOne(x => x.Category)
                    .WithMany(g => g.TrackCategories)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                tg.ToTable("TrackCategories");
            });
        });


        modelBuilder.Entity<TrackLike>(entity =>
        {
            entity.HasKey(e => new { e.TrackId, e.UserId }).HasName("PK__TrackLik__AB0C74247035CAF8");

            entity.Property(e => e.LikedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Track).WithMany(p => p.TrackLikes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TrackLike__Track__00200768");

            entity.HasOne(d => d.User).WithMany(p => p.TrackLikes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TrackLike__UserI__01142BA1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
