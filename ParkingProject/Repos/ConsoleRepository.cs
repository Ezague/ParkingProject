using System.Diagnostics;
using System.Text.RegularExpressions;
using ParkingProject.BLL;
using ParkingProject.Data;
using ParkingProject.Models;

namespace ParkingProject.Repos;

public class ConsoleRepository {

    public static void PrintMenu(ParkingLot parkingLot)
    {
        Console.WriteLine($"------ {parkingLot.Name} ------");
        Console.WriteLine("P. Purchase parking space");
        Console.WriteLine("B. Buy car wash");
        Console.WriteLine("S. See spaces and prices");
        Console.WriteLine("L. Pay lease");
        Console.WriteLine("A. Admin menu");
        Console.WriteLine("X. Exit");
        Console.WriteLine("-------------------------");
    }

    public static void BuyParkingSpace(ParkingLot parkingLot)
    {
        bool hasPurchasedCarWash = false;
        Console.Write("Enter license plate: ");
        string licensePlate = Console.ReadLine().ToUpper();
        Console.Write("Define vehicle type: 0. Normal, 1. Handicap, 2. Bus, 3. Motorcycle: ");
        int vehicleType = int.TryParse(Console.ReadLine(), out vehicleType) ? vehicleType : 0;
        ParkingSpaceTypes parkingSpaceTypes = (ParkingSpaceTypes)vehicleType;

        Console.WriteLine("Do you want to buy a wash for your car? Y/N: ");
            ConsoleKeyInfo valg = Console.ReadKey(true);
            if (valg.Key == ConsoleKey.Y)
            {
                hasPurchasedCarWash = true;
            }
        Regex rx = new(@"[A-Z]{2}\s*[0-9]{2}\s*[0-9]{3}");

        if (rx.IsMatch(licensePlate))
        {
            Console.Clear();
            Console.WriteLine(parkingLot.ParkingLotRepository.LeaseParkingSpace(parkingSpaceTypes, licensePlate, parkingLot.ParkingSpaceList, hasPurchasedCarWash));
            Console.ReadKey(true);
        }
        else
        {
            Console.WriteLine("Invalid license plate");
            Console.ReadKey(true);
        }
    }

    public static void PayLease(ParkingLot parkingLot)
    {
        Console.Write("Enter license plate: ");
        string licensePlate = Console.ReadLine().ToUpper();

        Console.Clear();
        Console.WriteLine(parkingLot.ParkingLotRepository.PayLease(licensePlate, parkingLot));
        Console.ReadKey(true);
    }

    public static void ShowSpacesAndPrices(ParkingLot parkingLot)
    {
        Console.Clear();
        Console.WriteLine("----------------------- Spaces and prices -----------------------");
        foreach (ParkingSpace space in parkingLot.ParkingSpaceList)
        {
            string availTemp = space.IsAvailable ? "Free" : $"Leased by {space.LicensePlate}";
            Console.WriteLine($"Space ID: {space.Id} - Type: {space.Type} - State: {availTemp} - {space.Price:C}");
            Console.WriteLine("-----------------------------------------------------------------");
        }
        Console.ReadKey(true);
    }

    public static async Task WashCarAsync(ParkingLot parkingLot)
    {
        Console.Write("Enter license plate: ");
        string licensePlate = Console.ReadLine().ToUpper();

        IParkingSpace tempSpace = parkingLot.ParkingSpaceList.FirstOrDefault(x => x.LicensePlate == licensePlate);

        if (tempSpace == null)
        {
            Console.WriteLine("No such license plate");
            Console.ReadKey(true);
            return;
        }
        if (tempSpace.PurchasedCarWash == false)
        {
            Console.Write("No car wash purchased, do you want to buy? Y/N: ");
            ConsoleKeyInfo valg = Console.ReadKey(true);
            switch (valg.Key)
            {
                case ConsoleKey.Y:
                    tempSpace.PurchasedCarWash = true;
                    break;
                case ConsoleKey.N:
                    return;
                default: return;
            }
        }
        await parkingLot.CarWash.WashCarAsync(licensePlate);
    }

    public static void PrintAdminMenu(ParkingLot parkingLot)
    {
        bool isLoggedIn = false;
        string username = string.Empty;
        Admin admin = new("admin", "1234");

        // Login logic
        for (int i = 2; i >= 0; i--)
        {
            Console.Clear();
            Console.WriteLine("Admin menu - please log in");
            Console.Write("Username: ");
            username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (admin.IsLoggedIn(username, password))
            {
                isLoggedIn = true;
                break;
            }
            else
            {
                Console.WriteLine($"Wrong usernamd or password, you have {i} tries left");
                Console.ReadKey(true);
            }
        }

        while (isLoggedIn)
        {
            Console.Clear();
            Console.WriteLine($"------ Welcome {username} ------");
            Console.WriteLine("Y. Adjust prices for parking spaces");
            Console.WriteLine("C. Adjust price for car wash");
            Console.WriteLine("X. Exit");
            Console.WriteLine("-------------------------");

            ConsoleKeyInfo valg = Console.ReadKey(true);
            switch (valg.Key)
            {
                case ConsoleKey.Y:
                    AdjustParkingPricesAsync(parkingLot);
                    break;
                case ConsoleKey.C:
                    AdjustWashingPrices(parkingLot);
                    break;
                case ConsoleKey.X:
                    isLoggedIn = false;
                    break;
            }
        }
    }

    public static async Task AdjustParkingPricesAsync(ParkingLot parkingLot)
    {
        Console.Clear();
        Console.Out.WriteLineAsync($"Current prices for parking lot: {parkingLot.Name}\nNormal space: {parkingLot.ParkingPrices[0]:C}\nHandicap space: {parkingLot.ParkingPrices[1]:C}\nBus space: {parkingLot.ParkingPrices[2]:C}\nMotorcycle space: {parkingLot.ParkingPrices[3]:C}\n");
        Console.Out.WriteLineAsync("Select which price to adjust: 0. Normal, 1. Handicap, 2. Bus, 3. Motorcycle");
        if (!int.TryParse(Console.ReadLine(), out int valg))
        {
            Console.WriteLine("Please select a valid choice");
            Console.ReadKey(true);
        }
        else
        {
            Console.Out.WriteLineAsync($"Enter new price: ");
            double newPrice = double.TryParse(Console.ReadLine(), out newPrice) ? newPrice : parkingLot.ParkingPrices[valg];
            int i = 0;
            foreach(IParkingSpace space in parkingLot.ParkingSpaceList.Where(x => x.Type == (ParkingSpaceTypes)valg && x.IsAvailable == true)) 
            {
                space.Price = newPrice;
                i++;
            }
            parkingLot.ParkingPrices[valg] = newPrice;
            Console.Out.WriteLineAsync($"New price for {parkingLot.ParkingSpaceList[valg].Type} is now {parkingLot.ParkingPrices[valg]:C}\nChanged price on {i} spaces");
            Console.ReadKey(true);   
        }
    }

    public static void AdjustWashingPrices(ParkingLot parkingLot)
    {
        Console.Clear();
        Console.WriteLine("Current price for car wash: {0:C}", parkingLot.CarWash.Price);
        Console.Write("Enter new price: ");
        double newPrice = double.TryParse(Console.ReadLine(), out newPrice) ? newPrice : parkingLot.CarWash.Price;
        Console.WriteLine(parkingLot.CarWash.AdjustWashingPrice(newPrice));
        Console.ReadKey(true);
    }
}