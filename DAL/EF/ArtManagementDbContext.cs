using System.Diagnostics;
using ArtManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ArtManagement.DAL.EF;

public class ArtManagementDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Artwork> Artworks { get; set; }
    public DbSet<Museum> Museums { get; set; }
    public DbSet<ArtistArtwork> ArtistArtworks { get; set; }
    
    public ArtManagementDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=ArtManagement.db");
        }

        optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Artist>()
            .HasMany(artists => artists.Artworks)
            .WithOne(artwork => artwork.Artist);
        
        modelBuilder.Entity<Artwork>()
            .HasMany(artwork => artwork.Artists)
            .WithOne(artist => artist.Artwork);
        
        modelBuilder.Entity<Artwork>()
            .HasOne(artwork => artwork.Museum)
            .WithMany(museum => museum.Artworks);

        modelBuilder.Entity<ArtistArtwork>()
            .Property("ArtistId");
        modelBuilder.Entity<ArtistArtwork>()
            .Property("ArtworkId");
        modelBuilder.Entity<ArtistArtwork>()
            .HasKey("ArtistId","ArtworkId");
    }

    public bool CreateDatabase(bool dropDatabase)
    {
        if (dropDatabase)
        {
            Database.EnsureDeleted();
        }
        
        return Database.EnsureCreated();
    }
}