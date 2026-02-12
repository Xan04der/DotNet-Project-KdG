using System.ComponentModel.DataAnnotations;
using ArtManagement.BL;
using ArtManagement.BL.Domain;
using ArtManagement.UI.CA.Extensions;

namespace ArtManagement.UI.CA;

public class ConsoleUi
{
    private readonly IManager _manager;
    public ConsoleUi(IManager manager)
    {
        _manager = manager;
    }
    public void Run()
    {
        var exit = false;

        while (!exit)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("==========================");
            Console.WriteLine("0) Quit");
            Console.WriteLine("1) Show all artists");
            Console.WriteLine("2) Show artist by last name and/or date of birth");
            Console.WriteLine("3) Show all artworks");
            Console.WriteLine("4) Show artwork of art movement");
            Console.WriteLine("5) Create new artist");
            Console.WriteLine("6) Create new artwork");
            Console.WriteLine("7) Add artist to artwork");
            Console.WriteLine("8) Remove artist from artwork");
            Console.WriteLine("Make your choice:");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    exit = true;
                    break;
                case "1":
                    foreach (var artist in _manager.GetAllArtistsWithArtworks())
                    {
                        Console.WriteLine(artist.GetStringInfo());
                    }
                    break;
                case "2":
                    ArtistsFilter();
                    break;
                case "3":
                    foreach (var artwork in _manager.GetAllArtworksWithMuseum())
                    {
                        Console.WriteLine(artwork.GetStringInfo());
                    }
                    break;
                case "4":
                    Console.WriteLine("Movement (1=Baroque, 2=Impressionism, 3=Post Impressionism, 4=Surrealism):");
                    var movementInput = Console.ReadLine();
                    EnumFilter(movementInput);
                    break;
                case "5":
                    AddArtist();
                    break;
                case "6":
                    AddArtwork();
                    break;
                case "7":
                    AddArtistArtwork();
                    break;
                case "8":
                    RemoveArtistArtwork();
                    break;
            }
        }
    }

    private void ArtistsFilter()
    {
        Console.WriteLine("Enter a (optional) last name:");
        String minNameFilter = Console.ReadLine();
        
        Console.WriteLine("Enter a (optional) full date:");
        DateOnly? minDateFilter = ParseNullableDateOnly(Console.ReadLine());

        var filteredArtists = _manager.GetArtistsByNameAndOrBirthday(minNameFilter, minDateFilter);
        
        Console.WriteLine("Filtered Artists:");
        foreach (var  artist in filteredArtists)
        {
            Console.WriteLine(artist.GetStringInfo());
        }


    }
    
    private DateOnly? ParseNullableDateOnly(string input)
    {
        if (DateOnly.TryParse(input, out DateOnly result))
        {
            return result;
        }

        return null;
    }

    private void EnumFilter(String input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            input = "\n";
        }

        if (Enum.TryParse(input, true, out ArtMovementEnum artMovementEnum))
        {
            //input ok
            IEnumerable<Artwork> filteredArtworks = _manager.GetAllArtworksByArtMovement(artMovementEnum);

            foreach (var a in filteredArtworks)
            {
                Console.WriteLine(a.GetStringInfo());
            }
        }
    }

    private void AddArtist()
    {

        bool validItem = false;
        while (!validItem)
        {
            try
            {
                Console.WriteLine("Enter the artists firstname:");
                var firstName = Console.ReadLine();
                
                Console.WriteLine("Enter the artists lastname:");
                var name = Console.ReadLine();

                int age;
                string ageText;
                do
                {
                    Console.WriteLine("Enter the artists age:");
                    ageText = Console.ReadLine();
                } while (!Int32.TryParse(ageText, out age));

                DateOnly birthday;
                string dateText;
                do
                {
                   Console.WriteLine("Enter the date of birth:");
                   dateText = Console.ReadLine();
                } while (!DateOnly.TryParse(dateText, out birthday));

                DateOnly? deathday = null;
                string deathdayText;
                do
                {
                    Console.WriteLine("Enter the date of death (leave blank if not applicable):");
                    deathdayText = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(deathdayText))
                    {
                        break;
                    }
                } while (!DateOnly.TryParse(deathdayText, out DateOnly tempDeathday) || (deathday = tempDeathday) == null);
                
                Console.WriteLine();
                Console.WriteLine($"You gave this as data: {firstName} / {name} / {age} / {birthday} / {deathday}");
                
                _manager.AddArtist(firstName, name, age, birthday, deathday, "xander@kdg.be");
                validItem = true;
            }
            catch (ValidationException e)
            {
                Console.WriteLine("Error:" + e.Message);
                Console.WriteLine();
            }
            
        }
    }

    private void AddArtwork()
    {
        bool validItem = false;
        while (!validItem)
        {
            try
            {
                Console.WriteLine("Enter the artworks name:");
                var name = Console.ReadLine();
                
                double width;
                string widthText;
                do
                {
                    Console.WriteLine("Enter the artworks width:");
                    widthText = Console.ReadLine();
                } while (!Double.TryParse(widthText, out width));

                double length;
                string lengthText;
                do
                {
                    Console.WriteLine("Enter the artworks length:");
                    lengthText= Console.ReadLine();
                } while (!Double.TryParse(lengthText, out length));

                int? yearPainted = null;
                string yearText;
                do
                {
                    Console.WriteLine("Enter the year it was painted:");
                    yearText = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(yearText))
                    { 
                        break;   
                    }
                    
                } while (!Int32.TryParse(yearText, out int tempYearPainted) || (yearPainted = tempYearPainted) == null);
                
                ArtMovementEnum artMovement;
                string artMovementText;
                do
                {
                    Console.WriteLine("Enter the artworks art movement:");
                    foreach (var artM in Enum.GetValues<ArtMovementEnum>() )
                    {
                        Console.WriteLine(artM);
                        
                    }
                    Console.WriteLine("): ");
                    artMovementText = Console.ReadLine();
                } while (!Enum.TryParse(artMovementText, out artMovement));
                
                Console.WriteLine();
                Console.WriteLine($"You gave this as data: {name} / {width} / {length} / {yearPainted} / {artMovement}");
                
                _manager.AddArtwork(name, width, length, yearPainted, artMovement);
                validItem = true;
            }
            catch (ValidationException e)
            {
                Console.WriteLine("Error:" + e.Message);
                Console.WriteLine();
            }
            
        }
    }

    private void AddArtistArtwork()
    {
        IEnumerable<Artist> artists = _manager.GetAllArtists();
        IEnumerable<Artwork> artworks = _manager.GetAllArtworks();
        Console.WriteLine("Which artist would you like to add an artwork to?");
        foreach (var artist in artists)
        {
            Console.WriteLine("["+ artist.ArtistId + "] " + artist.FirstName + " " + artist.LastName);
        }

        Console.WriteLine("Please enter an artist ID: ");
        if (int.TryParse(Console.ReadLine(), out var artistId))
        {
            Console.WriteLine(" Which artwork would you like to assign to this artist?");
            foreach (var artwork in artworks)
            {
                Console.WriteLine("["+ artwork.ArtworkId + "] " + artwork.Name);
            }
            Console.WriteLine("Please enter an artwork ID: ");
            if (int.TryParse(Console.ReadLine(), out var artworkId))
            {
                Console.WriteLine("Enter the timeframe this artist has worked on the artwork:");
                var timeFrame = Console.ReadLine();
                
                bool tutor;
                string tutorText;
                do
                {
                    Console.WriteLine("Was this artist a tutor or a student when he worked on this artwork?");
                    tutorText = Console.ReadLine();
                } while (!bool.TryParse(tutorText, out tutor));
                
                _manager.AddArtistArtwork(artistId, artworkId, tutor, timeFrame);

            }
            
        }
    }

    private void RemoveArtistArtwork()
    {
        IEnumerable<Artist> artists = _manager.GetAllArtists();
        Console.WriteLine("Which artist would you like to remove an artwork from?");
        foreach (var artist in artists)
        {
            Console.WriteLine("["+ artist.ArtistId + "] " + artist.FirstName + " " + artist.LastName);
        }
        Console.WriteLine("Please enter an artist ID: ");
        if (int.TryParse(Console.ReadLine(), out var artistId))
        {
            Console.WriteLine("Which artwork would you like to remove from this artist?");
            foreach (var artwork in _manager.GetArtworksWithArtist(artistId))
            {
                Console.WriteLine("["+ artwork.ArtworkId + "] " + artwork.Name);
            }
            Console.WriteLine("Please enter an artwork ID: ");
            if (int.TryParse(Console.ReadLine(), out var artworkId))
            {
                _manager.DeleteArtistArtwork(artistId, artworkId);
            }
        }
    }
}