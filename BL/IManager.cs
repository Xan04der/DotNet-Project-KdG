using ArtManagement.BL.Domain;

namespace ArtManagement.BL;

public interface IManager
{
    public Artist GetArtist(int id);
    
    public IEnumerable<Artist> GetAllArtistsWithUser();
    public IEnumerable<Artist> GetAllArtists();

    public IEnumerable<Artist> GetArtistsByNameAndOrBirthday(string name, DateOnly? birthday);

    public Artist GetArtistWithArtworks(int id);
    
    public Artist GetArtistWithArtworksAndUser(int id);
    
    public IEnumerable<Artist> GetAllArtistsWithArtworks();
    
    public Artist AddArtist(string firstName, string lastName, int age, DateOnly birthday, DateOnly? deathday, string userId);
    
    public void UpdateArtist(Artist artist);

    public Artwork GetArtwork(int id);
    
    public IEnumerable<Artwork> GetAllArtworks();
    
    public IEnumerable<Artwork> GetAllArtworksByArtMovement(ArtMovementEnum artMovement);

    public IEnumerable<Artwork> GetAllArtworksWithMuseum();
    
    public Artwork AddArtwork(string name, double width, double length, int? yearPainted, ArtMovementEnum artMovement);
    
    public ArtistArtwork AddArtistArtwork(int artistId, int artworkId, bool tutor, string timeFrame);
    
    public ArtistArtwork DeleteArtistArtwork(int artistId,int artworkId);
    
    public IEnumerable<ArtistArtwork> GetArtistsOfArtwork(int artistId);
    
    public Museum GetMuseum(int id);
    
    public IEnumerable<Artwork> GetArtworksWithArtist(int artistId);
    
    public IEnumerable<Museum> GetAllMuseumsWithArtworks();
    
    public Museum AddMuseum(string name, string location, int yearEstablished);
}