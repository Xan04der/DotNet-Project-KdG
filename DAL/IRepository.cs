using ArtManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;

namespace ArtManagement.DAL;

public interface IRepository
{
    public Artist ReadArtist(int id);
    
    public IEnumerable<Artist> ReadAllArtistsWithUser();
    
    public IEnumerable<Artist> ReadAllArtists();
    
    public Artist ReadArtistWithArtworks(int id);
    
    public Artist ReadArtistWithArtworksAndUsers(int id);

    public IEnumerable<Artist> ReadAllArtistsWithArtworks();

    public IEnumerable<Artist> ReadArtistsByNameAndOrBirthday(string name, DateOnly? birthday);
    
    public void CreateArtist(Artist artist);

    public void UpdateArtist(Artist artist);
    
    public Artwork ReadArtwork(int id);
    
    public IEnumerable<Artwork> ReadAllArtworks();

    public IEnumerable<Artwork> ReadAllArtworksWithMuseum();
    
    public IEnumerable<Artwork> ReadAllArtworksByArtMovement(ArtMovementEnum artMovement);
    
    public void CreateArtwork(Artwork artwork);
    
    public void CreateArtistArtwork(ArtistArtwork artistArtwork);
    
    public void DeleteArtistArtwork(ArtistArtwork artistArtwork);
    
    public ArtistArtwork ReadArtistArtwork(int artistId, int artworkId);
    
    public IEnumerable<ArtistArtwork> ReadArtistsOfArtwork(int artworkId);
    
    public Museum ReadMuseum(int museumId);
    public IEnumerable<Artwork> ReadArtworksByArtist(int artistId);
    
    public IEnumerable<Museum> ReadAllMuseumsWithArtworks();
    
    public void CreateMuseum(Museum museum);
    
    public IdentityUser ReadUser(string userId);
}