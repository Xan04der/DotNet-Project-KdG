using ArtManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;

namespace ArtManagement.DAL;

public class InMemoryRepository : IRepository
{
    private static readonly ICollection<Artist> _artists = new List<Artist>();
    private static readonly ICollection<Artwork> _artworks = new List<Artwork>();

    public Artist ReadArtist(int id)
    {
        var result = new List<Artist>();
        foreach (var artist in _artists)
        {
            if (artist.ArtistId == id)
            {
               result.Add(artist); 
            } 
        }

        return null;
    }

    public IEnumerable<Artist> ReadAllArtistsWithUser()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Artist> ReadAllArtists()
    {
        return _artists;
    }

    public Artist ReadArtistWithArtworks(int id)
    {
        throw new NotImplementedException();
    }

    public Artist ReadArtistWithArtworksAndUsers(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Artist> ReadAllArtistsWithArtworks()
    {
        throw new NotImplementedException();
    }
    
    public IEnumerable<Artist> ReadArtistsByNameAndOrBirthday(string name, DateOnly? birthday)
    {
        return _artists.Where(artist => string.IsNullOrEmpty(name) && birthday == null ||
                                        name.Equals(artist.LastName.ToString(), StringComparison.OrdinalIgnoreCase) &&
                                        birthday == artist.BirthDate ||
                                        string.IsNullOrEmpty(name) && birthday == artist.BirthDate ||
                                        name.Equals(artist.LastName.ToString(), StringComparison.OrdinalIgnoreCase) && birthday == null);
    }

    public void CreateArtist(Artist artist)
    {
        artist.ArtistId = _artists.Count + 1;
        _artists.Add(artist);
    }

    public void UpdateArtist(Artist artist)
    {
        throw new NotImplementedException();
    }

    public Artwork ReadArtwork(int id)
    {
        var result = new List<Artwork>();
        foreach (var artwork in _artworks)
        {
            if (artwork.ArtworkId == id)
            {
                result.Add(artwork);
            }
        }

        return null;
        //return result;
    }
    
    public IEnumerable<Artwork> ReadAllArtworks()
    {
        return _artworks;
    }

    public IEnumerable<Artwork> ReadAllArtworksWithMuseum()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Artwork> ReadAllArtworksByArtMovement(ArtMovementEnum artMovement)
    {
        return _artworks.Where(artwork => artwork.ArtMovement == artMovement);
    }

    public void CreateArtwork(Artwork artwork)
    {
        artwork.ArtworkId = _artworks.Count + 1;
        _artworks.Add(artwork);
    }

    public void CreateArtistArtwork(ArtistArtwork artistArtwork)
    {
        throw new NotImplementedException();
    }

    public void DeleteArtistArtwork(ArtistArtwork artistArtwork)
    {
        throw new NotImplementedException();
    }

    public ArtistArtwork ReadArtistArtwork(int artistId, int artworkId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ArtistArtwork> ReadArtistsOfArtwork(int artworkId)
    {
        throw new NotImplementedException();
    }

    public Museum ReadMuseum(int museumId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Artwork> ReadArtworksByArtist(int artistId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Museum> ReadAllMuseumsWithArtworks()
    {
        throw new NotImplementedException();
    }

    public void CreateMuseum(Museum museum)
    {
        throw new NotImplementedException();
    }

    public IdentityUser ReadUser(string userId)
    {
        throw new NotImplementedException();
    }

    public static void Seed()
    {
        var rubens = new Artist
        {
            ArtistId = 1,
            FirstName = "Peter Paul",
            LastName = "Rubens",
            Age = 62,
            BirthDate = new DateOnly(1577,06,28),
            DeathDate = new DateOnly(1640,05,30),
            Artworks = new List<ArtistArtwork>()

        };

        var monet = new Artist
        {
            ArtistId = 2,
            FirstName = "Oscar-Claude",
            LastName = "Monet",
            Age = 62,
            BirthDate = new DateOnly(1840,11,14),
            DeathDate = new DateOnly(1926,12,05),
            Artworks = new List<ArtistArtwork>()

        };
        
        var vanGogh = new Artist
        {
            ArtistId = 3,
            FirstName = "Vincent",
            LastName = "van Gogh",
            Age = 37,
            BirthDate = new DateOnly(1853,03,30),
            DeathDate = new DateOnly(1890,07,29),
            Artworks = new List<ArtistArtwork>()

        };
        
        var dali = new Artist
        {
            ArtistId = 4,
            FirstName = "Salvador",
            LastName = "Dalí",
            Age = 84,
            BirthDate = new DateOnly(1904,05,11),
            DeathDate = new DateOnly(1989,01,23),
            Artworks = new List<ArtistArtwork>()

        };
        
        var maria = new Artwork
        {
            ArtworkId = 1,
            Name = "De Tenhemelopneming van Maria",
            Width = 4.9,
            Length = 3.25,
            ArtMovement = ArtMovementEnum.Baroque,
            YearPainted = 1626,
            Artists = new List<ArtistArtwork>()

        };
        
        var japBrug = new Artwork
        {
            ArtworkId = 2,
            Name = "De Japanse brug",
            Width = 0.93,
            Length = 0.74,
            YearPainted = 1899,
            ArtMovement = ArtMovementEnum.Impressionism,
            Artists = new List<ArtistArtwork>()

        };
        
        var sterWacht = new Artwork
        {
            ArtworkId = 3,
            Name = "De sterrenwacht",
            Width = 0.73,
            Length = 0.92,
            YearPainted = 1889,
            ArtMovement = ArtMovementEnum.Postimpressionism,
            Artists = new List<ArtistArtwork>()
        };
        
        var volhard = new Artwork
        {
            ArtworkId = 4,
            Name = "De volharding der herinnering",
            Width = 0.24,
            Length = 0.33,
            YearPainted = 1931,
            ArtMovement = ArtMovementEnum.Surrealism,
            Artists = new List<ArtistArtwork>()
        };
        
        //rubens.Artworks.Add(maria);
        //monet.Artworks.Add(japBrug);
        //vanGogh.Artworks.Add(sterWacht);
        //dali.Artworks.Add(volhard);
        _artworks.Add(maria);
        _artworks.Add(japBrug);
        _artworks.Add(sterWacht);
        _artworks.Add(volhard);
        
        _artists.Add(rubens);
        _artists.Add(monet);
        _artists.Add(vanGogh);
        _artists.Add(dali);
    }

}