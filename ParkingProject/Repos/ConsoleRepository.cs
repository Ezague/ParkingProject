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

        Console.Write("Do you want to buy a wash for your car? Y/N: ");
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
        }
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

    public static void WashCarAsync(ParkingLot parkingLot)
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
        parkingLot.CarWash.WashCarAsync(licensePlate);
    }
}