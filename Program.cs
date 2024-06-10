using System;
using System.Collections.Generic;
using System.Linq;

delegate void MoveDelegate(double trackLength, int laps);

interface IRaceVehicle
{
    string Name { get; set; }
    TimeSpan RaceTime { get; set; } 
    void SetRandomSpeed();
    void Move(double trackLength, int laps);
}

abstract class Vehicle : IRaceVehicle
{
    public string Name { get; set; }
    public int MinSpeed { get; set; }
    public int MaxSpeed { get; set; }
    public TimeSpan RaceTime { get; set; } 

    public abstract void Move(double trackLength, int laps);

    public void SetRandomSpeed()
    {
        Random random = new Random();
        Speed = random.Next(MinSpeed, MaxSpeed + 1);
    }

    public int Speed { get; private set; }
}

class Car : Vehicle
{
    public Car(string name, int minSpeed, int maxSpeed)
    {
        Name = name;
        MinSpeed = minSpeed;
        MaxSpeed = maxSpeed;
    }

    public override void Move(double trackLength, int laps)
    {
        SetRandomSpeed();
        double totalRaceLength = trackLength * laps;
        double timeInSeconds = (totalRaceLength / Speed) * 3600;
        RaceTime = TimeSpan.FromSeconds(timeInSeconds);
        Console.WriteLine($"{Name} porusza się z prędkością {Speed} km/h i ukończył wyścig w czasie {RaceTime.Hours:D2}:{RaceTime.Minutes:D2}:{RaceTime.Seconds:D2}.");
    }
}

class Motorcycle : Vehicle
{
    public Motorcycle(string name, int minSpeed, int maxSpeed)
    {
        Name = name;
        MinSpeed = minSpeed;
        MaxSpeed = maxSpeed;
    }

    public override void Move(double trackLength, int laps)
    {
        SetRandomSpeed();
        double totalRaceLength = trackLength * laps;
        double timeInSeconds = (totalRaceLength / Speed) * 3600;
        RaceTime = TimeSpan.FromSeconds(timeInSeconds);
        Console.WriteLine($"{Name} porusza się z prędkością {Speed} km/h i ukończył wyścig w czasie {RaceTime.Hours:D2}:{RaceTime.Minutes:D2}:{RaceTime.Seconds:D2}.");
    }
}

class RaceTrack
{
    public string Name { get; set; }
    public double Length { get; set; }
    public int Laps { get; set; }

    public RaceTrack(string name, double length, int laps)
    {
        Name = name;
        Length = length;
        Laps = laps;
    }
}

class Program
{
    static void Main()
    {
        bool exitProgram = false;

        do
        {
            Console.WriteLine("Witaj w Symulatorze Wyścigów!");
            Console.WriteLine("Co chesz zrobić?");
            Console.WriteLine("1. Wybierz tor wyścigowy");
            Console.WriteLine("2. Zobacz poprzednie wyniki");
            Console.WriteLine("3. Wyłącz program");

            int mainMenuChoice;
            while (!int.TryParse(Console.ReadLine(), out mainMenuChoice) || (mainMenuChoice < 1 || mainMenuChoice > 3))
            {
                Console.WriteLine("Nieprawidłowy wybór. Wybierz ponownie.");
            }

            switch (mainMenuChoice)
            {
                case 1:
                    PlayRace();
                    break;
                case 2:
                    ShowPreviousResults();
                    break;
                case 3:
                    exitProgram = true;
                    break;
                default:
                    break;
            }

            Console.Clear();

        } while (!exitProgram);
    }
    static void PlayRace()
    {
        List<RaceTrack> raceTracks = new List<RaceTrack>
    {
        new RaceTrack("Circuit de Spa-Francorchamps", 7, 44),
        new RaceTrack("Suzuka Circuit", 6, 53),
        new RaceTrack("Nürburgring", 20, 150),
        new RaceTrack("Autódromo De Interlagos", 4, 71),
        new RaceTrack("Autodromo Nazionale Monza", 6, 53),
        new RaceTrack("Red Bull Ring", 200, 2),
        new RaceTrack("Circuit de Barcelona-Catalunya – układ Grand Prix", 5, 66),
        new RaceTrack("Circuit de la Sarthe", 14, 350)
    };

        Console.WriteLine("Dostępne tory wyścigowe:");
        for (int i = 0; i < raceTracks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {raceTracks[i].Name} (Długość: {raceTracks[i].Length} km, Okrążenia: {raceTracks[i].Laps})");
        }

        Console.WriteLine($"{raceTracks.Count + 1}. Powrót do menu głównego");

        int trackChoice;
        while (!int.TryParse(Console.ReadLine(), out trackChoice) || trackChoice < 1 || trackChoice > raceTracks.Count + 1)
        {
            Console.WriteLine("Nieprawidłowy wybór. Wybierz ponownie.");
        }

        if (trackChoice == raceTracks.Count + 1)
        {
            return;
        }

        Console.WriteLine("Wybierz kategorię pojazdu:");
        Console.WriteLine("1. Samochód");
        Console.WriteLine("2. Motocykl");
        Console.WriteLine("3. Powrót");

        int categoryChoice;
        while (!int.TryParse(Console.ReadLine(), out categoryChoice) || categoryChoice < 1 || categoryChoice > 3)
        {
            Console.WriteLine("Nieprawidłowy wybór. Wybierz ponownie.");
        }

        List<IRaceVehicle> vehiclesInRace = new List<IRaceVehicle>();

        IRaceVehicle? userSelectedVehicle = null;
        switch (categoryChoice)
        {
            case 1:
                Console.WriteLine("Wybierz samochód:");
                Console.WriteLine("1. Ferrari 296 GT3");
                Console.WriteLine("2. Porsche 911 GT3 R");
                Console.WriteLine("3. Lamborghini Huracán GT3 Evo 2");
                Console.WriteLine("4. BMW M4 GT3");
                Console.WriteLine("5. McLaren 720S GT3");
                Console.WriteLine("6. Aston Martin Vantage AMR GT3");
                Console.WriteLine("7. Bentley Continental GT3");
                Console.WriteLine("8. Ford GT GT3");
                Console.WriteLine("9. Nissan GT-R Nismo GT3");
                Console.WriteLine("10. Chevrolet Camaro GT3");

                int carChoice;
                while (!int.TryParse(Console.ReadLine(), out carChoice) || carChoice < 1 || carChoice > 10)
                {
                    Console.WriteLine("Nieprawidłowy wybór. Wybierz ponownie.");
                }
                userSelectedVehicle = new Car("", 0, 0);
                switch (carChoice)
                {
                    case 1:
                        userSelectedVehicle = new Car("Ferrari 296 GT3", 200, 330);
                        break;
                    case 2:
                        userSelectedVehicle = new Car("Porsche 911 GT3 R", 200, 318);
                        break;
                    case 3:
                        userSelectedVehicle = new Car("Lamborghini Huracán GT3 Evo 2", 170, 310);
                        break;
                    case 4:
                        userSelectedVehicle = new Car("BMW M4 GT3", 200, 309);
                        break;
                    case 5:
                        userSelectedVehicle = new Car("McLaren 720S GT3", 200, 341);
                        break;
                    case 6:
                        userSelectedVehicle = new Car("Aston Martin Vantage AMR GT3", 200, 320);
                        break;
                    case 7:
                        userSelectedVehicle = new Car("Bentley Continental GT3", 200, 331);
                        break;
                    case 8:
                        userSelectedVehicle = new Car("Ford GT GT3", 200, 330);
                        break;
                    case 9:
                        userSelectedVehicle = new Car("Nissan GT-R Nismo GT3", 200, 315);
                        break;
                    case 10:
                        userSelectedVehicle = new Car("Chevrolet Camaro GT3", 200, 318);
                        break;

                    default:
                        break;
                }
                vehiclesInRace.Add(userSelectedVehicle);
                break;
            case 2:
                Console.WriteLine("Wybierz motocykl:");
                Console.WriteLine("1. Harley");
                Console.WriteLine("2. Ducati");
                Console.WriteLine("3. Honda");
                Console.WriteLine("4. Kawasaki");
                Console.WriteLine("5. Yamaha");
                Console.WriteLine("6. Suzuki");
                int motorcycleChoice;
                while (!int.TryParse(Console.ReadLine(), out motorcycleChoice) || motorcycleChoice < 1 || motorcycleChoice > 6)
                {
                    Console.WriteLine("Nieprawidłowy wybór. Wybierz ponownie.");
                }
                userSelectedVehicle = new Motorcycle("", 0, 0);
                switch (motorcycleChoice)
                {
                    case 1:
                        userSelectedVehicle = new Motorcycle("Harley", 130, 160);
                        break;
                    case 2:
                        userSelectedVehicle = new Motorcycle("Ducati", 140, 170);
                        break;
                    case 3:
                        userSelectedVehicle = new Motorcycle("Honda", 120, 150);
                        break;
                    case 4:
                        userSelectedVehicle = new Motorcycle("Kawasaki", 125, 155);
                        break;
                    case 5:
                        userSelectedVehicle = new Motorcycle("Yamaha", 135, 165);
                        break;
                    case 6:
                        userSelectedVehicle = new Motorcycle("Suzuki", 128, 158);
                        break;
                    default:
                        break;
                }
                vehiclesInRace.Add(userSelectedVehicle);
                break;
            case 3:
                return;
            default:
                break;
        }

        Random random = new Random();
        int numberOfRandomVehicles = 7;
        for (int i = 0; i < numberOfRandomVehicles; i++)
        {
            IRaceVehicle randomVehicle = categoryChoice == 1 ? GetRandomCar(random) : GetRandomMotorcycle(random);
            vehiclesInRace.Add(randomVehicle);
        }

        RaceTrack selectedRaceTrack = raceTracks[trackChoice - 1];

        Console.WriteLine($"Rozpoczynają się wyścigi na torze {selectedRaceTrack.Name}, długości okrążenia wynosi {selectedRaceTrack.Length} km!");
        Console.WriteLine($"Liczba okrążeń: {selectedRaceTrack.Laps}, Całkowita długość wyścigu: {selectedRaceTrack.Length * selectedRaceTrack.Laps}");

        foreach (var vehicle in vehiclesInRace)
        {
            Console.WriteLine($"\n{vehicle.Name} bierze udział w wyścigu!");
            vehicle.Move(selectedRaceTrack.Length, selectedRaceTrack.Laps);

            Thread.Sleep(1000); 
        }

        Console.WriteLine("\nWyniki końcowe:");

        Thread.Sleep(1000); 

        var sortedResults = vehiclesInRace.OrderBy(v => v.RaceTime).ToList();

        for (int i = 0; i < sortedResults.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.White;

            if (sortedResults[i] == userSelectedVehicle)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (i == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (i == 1)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (i == 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }

            Console.WriteLine($"{i + 1}. {sortedResults[i].Name} - {sortedResults[i].RaceTime.Hours:D2}:{sortedResults[i].RaceTime.Minutes:D2}:{sortedResults[i].RaceTime.Seconds:D2}");
        }

        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("Koniec wyścigu!\n");

        SaveResultsToFile(sortedResults, userSelectedVehicle, selectedRaceTrack);

        Console.WriteLine("Co chcesz teraz zrobić?");
        Console.WriteLine("1. Wróć do ekranu startowego");
        Console.WriteLine("2. Wyłącz program");

        int userChoice;
        while (!int.TryParse(Console.ReadLine(), out userChoice) || (userChoice != 1 && userChoice != 2))
        {
            Console.WriteLine("Nieprawidłowy wybór. Wybierz ponownie.");
        }

        if (userChoice == 2)
        {
            Environment.Exit(0);
        }

        Console.Clear();
    }
    static void SaveResultsToFile(List<IRaceVehicle> results, IRaceVehicle userSelectedVehicle, RaceTrack selectedRaceTrack)
    {
        string fileName = "results.txt";
        using (StreamWriter writer = new StreamWriter(fileName, true))
        {
            writer.WriteLine($"Wyniki wyścigu na torze {selectedRaceTrack.Name}, długość okrążenia: {selectedRaceTrack.Length} km, liczba okrążeń: {selectedRaceTrack.Laps}:");

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i] == userSelectedVehicle)
                {
                    writer.WriteLine($"{i + 1}. {results[i].Name} - {results[i].RaceTime.Hours:D2}:{results[i].RaceTime.Minutes:D2}:{results[i].RaceTime.Seconds:D2} <- Twój pojazd");
                }
                else
                {
                    writer.WriteLine($"{i + 1}. {results[i].Name} - {results[i].RaceTime.Hours:D2}:{results[i].RaceTime.Minutes:D2}:{results[i].RaceTime.Seconds:D2}");
                }
            }

            writer.WriteLine(); 
            writer.WriteLine(new string('-', 30)); 
            writer.WriteLine(); 
        }
    }
    static void ShowPreviousResults()
    {
        Console.WriteLine("\nPoprzednie wyniki:");

        if (File.Exists("results.txt"))
        {
            string[] lines = File.ReadAllLines("results.txt");
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("Brak poprzednich wyników.");
        }

        Console.WriteLine("\nNaciśnij dowolny klawisz, aby wrócić do menu głównego...");
        Console.ReadKey();
    }
    static Car GetRandomCar(Random random)
    {
        string[] carModels = { "Ferrari 296 GT3", "Porsche 911 GT3 R", "Lamborghini Huracán GT3 Evo 2", "BMW M4 GT3", "McLaren 720S GT3", "Aston Martin Vantage AMR GT3", "Bentley Continental GT3", "Ford GT GT3", "Nissan GT-R Nismo GT3", "Chevrolet Camaro GT3" };
        string randomCarModel = carModels[random.Next(carModels.Length)];
        int randomSpeed = random.Next(200, 310);
        return new Car(randomCarModel, randomSpeed - 20, randomSpeed);
    }

    static Motorcycle GetRandomMotorcycle(Random random)
    {
        string[] motorcycleModels = { "Harley", "Ducati", "Honda", "Kawasaki", "Yamaha", "Suzuki" };
        string randomMotorcycleModel = motorcycleModels[random.Next(motorcycleModels.Length)];
        int randomSpeed = random.Next(110, 170);
        return new Motorcycle(randomMotorcycleModel, randomSpeed - 10, randomSpeed);
    }
}
