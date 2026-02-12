using ArtManagement.BL.Domain;

namespace ArtManagement.DAL.EF;

public static class DataSeeder
{
    public static void Seed(ArtManagementDbContext dbContext)
    {
        var rubens = new Artist
        {
            ArtistId = 1,
            FirstName = "Peter Paul",
            LastName = "Rubens",
            Age = 62,
            BirthDate = new DateOnly(1577,06,28),
            DeathDate = new DateOnly(1640,05,30),
            Artworks = new List<ArtistArtwork>(),
            User = dbContext.Users.Single(u => u.Email == "xander@kdg.be")

        };

        var monet = new Artist
        {
            ArtistId = 2,
            FirstName = "Oscar-Claude",
            LastName = "Monet",
            Age = 62,
            BirthDate = new DateOnly(1840,11,14),
            DeathDate = new DateOnly(1926,12,05),
            Artworks = new List<ArtistArtwork>(),
            User = dbContext.Users.Single(u => u.Email == "jasper@kdg.be")

        };
        
        var vanGogh = new Artist
        {
            ArtistId = 3,
            FirstName = "Vincent",
            LastName = "van Gogh",
            Age = 37,
            BirthDate = new DateOnly(1853,03,30),
            DeathDate = new DateOnly(1890,07,29),
            Artworks = new List<ArtistArtwork>(),
            User = dbContext.Users.Single(u => u.Email == "xander@kdg.be")

        };
        
        var dali = new Artist
        {
            ArtistId = 4,
            FirstName = "Salvador",
            LastName = "Dalí",
            Age = 84,
            BirthDate = new DateOnly(1904,05,11),
            DeathDate = new DateOnly(1989,01,23),
            Artworks = new List<ArtistArtwork>(),
            User = dbContext.Users.Single(u => u.Email == "xander@kdg.be")

        };
        
        var cath = new Museum
        {
            MuseumId = 1,
            Name = "Onze-Lieve-Vrouwekathedraal",
            Location = "Antwerp",
            YearEstablished = 1352,
            Artworks = new List<Artwork>()

        };

        var natGal = new Museum
        {
            MuseumId = 2,
            Name = "National Gallery",
            Location = "London",
            YearEstablished = 1824,
            Artworks = new List<Artwork>()

        };

        var musModArt = new Museum
        {
            MuseumId = 3,
            Name = "Museum of Modern Art",
            Location = "New York",
            YearEstablished = 1929,
            Artworks = new List<Artwork>()
        };

        var kmska = new Museum
        {
            MuseumId = 4,
            Name = "Koninklijk Museum voor Schone Kunsten Antwerpen",
            Location = "Antwerp",
            YearEstablished = 1810,
            Artworks = new List<Artwork>()
        };
        
        var maria = new Artwork
        {
            ArtworkId = 1,
            Name = "De Tenhemelopneming van Maria",
            Width = 4.9,
            Length = 3.25,
            ArtMovement = ArtMovementEnum.Baroque,
            YearPainted = 1626,
            Artists = new List<ArtistArtwork>(),
            Museum = cath

        };
        
        var japBrug = new Artwork
        {
            ArtworkId = 2,
            Name = "De Japanse brug",
            Width = 0.93,
            Length = 0.74,
            YearPainted = 1899,
            ArtMovement = ArtMovementEnum.Impressionism,
            Artists = new List<ArtistArtwork>(),
            Museum = natGal

        };
        
        var sterWacht = new Artwork
        {
            ArtworkId = 3,
            Name = "De sterrennacht",
            Width = 0.73,
            Length = 0.92,
            YearPainted = 1889,
            ArtMovement = ArtMovementEnum.Postimpressionism,
            Artists = new List<ArtistArtwork>(),
            Museum = musModArt
        };
        
        var volhard = new Artwork
        {
            ArtworkId = 4,
            Name = "De volharding der herinnering",
            Width = 0.24,
            Length = 0.33,
            YearPainted = 1931,
            ArtMovement = ArtMovementEnum.Surrealism,
            Artists = new List<ArtistArtwork>(),
            Museum = musModArt
        };
        
        rubens.Artworks.Add(new ArtistArtwork
        {
            Artist = rubens,
            Artwork = maria,
            Tutor = true,
            TimeFrame = "1650 - ca.1655"
            
        });
        
        monet.Artworks.Add(new ArtistArtwork
        {
            Artist = monet,
            Artwork = japBrug,
            Tutor = true,
            TimeFrame = "1899"
        });
        
        vanGogh.Artworks.Add(new ArtistArtwork
        {
            Artist = vanGogh,
            Artwork = sterWacht,
            Tutor = true,
            TimeFrame = "juni 1889–juni 1889"
        });
        
        dali.Artworks.Add(new ArtistArtwork
        {
            Artist = dali,
            Artwork = volhard,
            Tutor = true,
            TimeFrame = "1931"
        });
        
        cath.Artworks.Add(maria);
        natGal.Artworks.Add(japBrug);
        musModArt.Artworks.Add(sterWacht);
        musModArt.Artworks.Add(volhard);
        
        dbContext.Artworks.Add(maria);
        dbContext.Artworks.Add(japBrug);
        dbContext.Artworks.Add(sterWacht);
        dbContext.Artworks.Add(volhard);
        
        dbContext.Artists.Add(rubens);
        dbContext.Artists.Add(monet);
        dbContext.Artists.Add(vanGogh);
        dbContext.Artists.Add(dali);

        dbContext.Museums.Add(cath);
        dbContext.Museums.Add(natGal);
        dbContext.Museums.Add(musModArt);
        dbContext.Museums.Add(kmska);

        dbContext.SaveChanges();
        
        dbContext.ChangeTracker.Clear();
    }
    
}