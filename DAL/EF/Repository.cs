using ArtManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArtManagement.DAL.EF;

public class Repository : IRepository
{
    private readonly ArtManagementDbContext _context;

    public Repository(ArtManagementDbContext context)
    {
        _context = context;
    }
    public Artist ReadArtist(int id)
    {
        return _context.Artists.SingleOrDefault(a => a.ArtistId == id);
    }

    public IEnumerable<Artist> ReadAllArtistsWithUser()
    {
        return _context.Artists
            .Include(artist => artist.User)
            .ToList();
    }

    public IEnumerable<Artist> ReadAllArtists()
    {
        return _context.Artists;
    }

    public Artist ReadArtistWithArtworks(int id)
    {
        return _context.Artists.Include(a => a.Artworks).ThenInclude(aa => aa.Artwork).SingleOrDefault(a => a.ArtistId == id);
    }

    public Artist ReadArtistWithArtworksAndUsers(int id)
    {
        return _context.Artists
            .Include(a => a.Artworks)
            .ThenInclude(aa => aa.Artwork)
            .Include(a => a.User)
            .Single(artist => artist.ArtistId == id);
    }

    public IEnumerable<Artist> ReadAllArtistsWithArtworks()
    {
        return _context.Artists.Include(artist => artist.Artworks)
            .ThenInclude(artistArtwork => artistArtwork.Artwork);
    }

    public IEnumerable<Artist> ReadArtistsByNameAndOrBirthday(string name, DateOnly? birthday)
    {
        IQueryable<Artist> result = _context.Artists;

        if (!string.IsNullOrEmpty(name))
        {
            result = result.Where(artist => artist.LastName.Contains(name));
        }

        if (birthday.HasValue)
        {
            result = result.Where(artist => artist.BirthDate == birthday);
        }

        return result.ToList();
    }

    public void CreateArtist(Artist artist)
    {
        _context.Artists.Add(artist);
        _context.SaveChanges();
    }

    public void UpdateArtist(Artist artist)
    {
        _context.Artists.Update(artist);
        _context.SaveChanges();
    }

    public Artwork ReadArtwork(int id)
    {
        return _context.Artworks.SingleOrDefault(a => a.ArtworkId == id);
    }
    public IEnumerable<Artwork> ReadAllArtworks()
    {
        return _context.Artworks;
    }

    public IEnumerable<Artwork> ReadAllArtworksWithMuseum()
    {
        return _context.Artworks.Include(artwork => artwork.Museum).ToList();
    }

    public IEnumerable<Artwork> ReadAllArtworksByArtMovement(ArtMovementEnum artMovement)
    {
        return _context.Artworks.Where(a => a.ArtMovement == artMovement).Include(artwork => artwork.Museum);
    }

    public void CreateArtwork(Artwork artwork)
    {
        _context.Artworks.Add(artwork);
        _context.SaveChanges();
    }

    public void CreateArtistArtwork(ArtistArtwork artistArtwork)
    {
        _context.ArtistArtworks.Add(artistArtwork);
        _context.SaveChanges();
    }

    public void DeleteArtistArtwork(ArtistArtwork artistArtwork)
    {
        _context.ArtistArtworks.Remove(artistArtwork);
        _context.SaveChanges();
    }

    public ArtistArtwork ReadArtistArtwork(int artistId, int artworkId)
    {
        return _context.ArtistArtworks
            .Include(aa => aa.Artist)
            .Include(aa => aa.Artwork)
            .FirstOrDefault(aa => aa.Artist.ArtistId == artistId && aa.Artwork.ArtworkId == artworkId);
    }

    public IEnumerable<ArtistArtwork> ReadArtistsOfArtwork(int artworkId)
    {
        return _context.ArtistArtworks
            .Include(aa => aa.Artist)
            .Where(aa => aa.Artwork.ArtworkId == artworkId)
            .ToList();
    }

    public Museum ReadMuseum(int museumId)
    {
        return _context.Museums.SingleOrDefault(m => m.MuseumId == museumId);

    }

    public IEnumerable<Artwork> ReadArtworksByArtist(int artistId)
    {
        return _context.Artworks.Include(artwork => artwork.Artists)
             .Where(artistArtwork => artistArtwork.ArtworkId == artistId);
    }

    public IEnumerable<Museum> ReadAllMuseumsWithArtworks()
    {
        return _context.Museums.Include(museum => museum.Artworks).ToList();
    }

    public void CreateMuseum(Museum museum)
    {
        _context.Museums.Add(museum);
        _context.SaveChanges();
    }

    public IdentityUser ReadUser(string userId)
    {
        return _context.Users
            .Find(userId);
    }
}