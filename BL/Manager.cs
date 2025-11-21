using System.ComponentModel.DataAnnotations;
using System.Text;
using ArtManagement.BL.Domain;
using ArtManagement.DAL;

namespace ArtManagement.BL;

public class Manager : IManager
{
    private readonly IRepository _repository;

    public Manager(IRepository repository)
    {
        _repository = repository;
    }

    public Artist GetArtist(int id)
    {
        return _repository.ReadArtist(id);
    }

    public IEnumerable<Artist> GetAllArtistsWithUser()
    {
        return _repository.ReadAllArtistsWithUser();
    }


    public IEnumerable<Artist> GetAllArtists()
    {
        return _repository.ReadAllArtists();
    }

    public IEnumerable<Artist> GetArtistsByNameAndOrBirthday(string name, DateOnly? birthday)
    {
        return _repository.ReadArtistsByNameAndOrBirthday(name, birthday);
    }

    public Artist GetArtistWithArtworks(int id)
    {
        return _repository.ReadArtistWithArtworks(id);
    }

    public Artist GetArtistWithArtworksAndUser(int id)
    {
        return _repository.ReadArtistWithArtworksAndUsers(id);
    }

    public IEnumerable<Artist> GetAllArtistsWithArtworks()
    {
        return _repository.ReadAllArtistsWithArtworks();
    }

    public Artist AddArtist(string firstName, string lastName, int age, DateOnly birthday, DateOnly? deathday, string userId)
    {
        var artist = new Artist
        {
            FirstName = firstName,
            LastName = lastName,
            Age = age,
            BirthDate = birthday,
            DeathDate = deathday,
            User = _repository.ReadUser(userId)
        };

        var validationResults = new List<ValidationResult>();
        var validationSuccess = Validator.TryValidateObject(artist, new ValidationContext(artist), validationResults,
            validateAllProperties: true);

        if (!validationSuccess)
        {
            var sb = new StringBuilder();
            foreach (var validationResult in validationResults)
            {
                sb.Append(validationResult.ErrorMessage + "\n");
            }
            throw new ValidationException(sb.ToString());
        }
        
        _repository.CreateArtist(artist);
        return artist;
    }

    public void UpdateArtist(Artist artist)
    {
        var errors = new List<ValidationResult>();
        
        Validator.TryValidateProperty(artist.Age,
            new ValidationContext(artist) {MemberName = nameof(Artist.Age)}, 
            errors);

        if (errors.Any())
        {
            string errorMessage = string.Join("|", errors.Select(error => error.ErrorMessage));
            throw new ValidationException(errorMessage);
        }
        
        _repository.UpdateArtist(artist);
    }

    public Artwork GetArtwork(int id)
    {
        return _repository.ReadArtwork(id);
    }
    
    public IEnumerable<Artwork> GetAllArtworks()
    {
        return _repository.ReadAllArtworks();
    }

    public IEnumerable<Artwork> GetAllArtworksByArtMovement(ArtMovementEnum artMovement)
    {
        return _repository.ReadAllArtworksByArtMovement(artMovement);
    }

    public IEnumerable<Artwork> GetAllArtworksWithMuseum()
    {
        return _repository.ReadAllArtworksWithMuseum();
    }

    public Artwork AddArtwork(string name, double width, double length, int? yearPainted, ArtMovementEnum artMovement)
    {
        var artwork = new Artwork
        {
            Name = name,
            Width = width,
            Length = length,
            YearPainted = yearPainted,
            ArtMovement = artMovement
        };
        
        var validationResults = new List<ValidationResult>();
        var validationSuccess = Validator.TryValidateObject(artwork, new ValidationContext(artwork), validationResults,
            validateAllProperties: true);

        if (!validationSuccess)
        {
            var sb = new StringBuilder();
            foreach (var validationResult in validationResults)
            {
                sb.Append(validationResult.ErrorMessage + "\n");
            }
            throw new ValidationException(sb.ToString());
        }
        
        _repository.CreateArtwork(artwork);
        return artwork;
    }

    public ArtistArtwork AddArtistArtwork(int artistId, int artworkId, bool tutor, string timeFrame)
    {
        var artist = _repository.ReadArtist(artistId);
        var artwork = _repository.ReadArtwork(artworkId);
        var artistArtwork = new ArtistArtwork
        {
            Artist = artist,
            Artwork = artwork,
            Tutor = tutor,
            TimeFrame = timeFrame
        };
        _repository.CreateArtistArtwork(artistArtwork);
        return artistArtwork;
    }

    public ArtistArtwork DeleteArtistArtwork(int artistId, int artworkId)
    {
        var artistArtwork = _repository.ReadArtistArtwork(artistId, artworkId);
        if (artistArtwork == null)
        {
            throw new InvalidOperationException($"The relationship between artist {artistId} and artwork {artworkId} does not exist.");
        }

        _repository.DeleteArtistArtwork(artistArtwork);
        return artistArtwork;
    }

    public IEnumerable<ArtistArtwork> GetArtistsOfArtwork(int artistId)
    {
        return _repository.ReadArtistsOfArtwork(artistId);
    }

    public Museum GetMuseum(int id)
    {
        return _repository.ReadMuseum(id);
    }

    public IEnumerable<Artwork> GetArtworksWithArtist(int artistId)
    {
        return _repository.ReadArtworksByArtist(artistId);
    }

    public IEnumerable<Museum> GetAllMuseumsWithArtworks()
    {
        return _repository.ReadAllMuseumsWithArtworks();
    }

    public Museum AddMuseum(string name, string location, int yearEstablished)
    {
        var museum = new Museum
        {
            Name = name,
            Location = location,
            YearEstablished = yearEstablished
        };
        
        var validationResults = new List<ValidationResult>();
        var validationSuccess = Validator.TryValidateObject(museum, new ValidationContext(museum), validationResults,
            validateAllProperties: true);

        if (!validationSuccess)
        {
            var sb = new StringBuilder();
            foreach (var validationResult in validationResults)
            { 
                sb.Append(validationResult.ErrorMessage + "\n");   
            }
            throw new ValidationException(sb.ToString());
        }
        _repository.CreateMuseum(museum);
        return museum;
    }
}