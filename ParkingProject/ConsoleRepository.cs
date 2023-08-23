using System.Text.RegularExpressions;
using ParkingProject.BLL;
using ParkingProject.Models;
namespace ParkingProject;

public class ConsoleRepository {

    public static void PrintMenu(ParkingLot parkingLot)
        {
            Console.WriteLine($"------ {parkingLot.Name} ------");
            Console.WriteLine("P. Purchase parking space");
            Console.WriteLine("B. Buy car wash");
            Console.WriteLine("S. See spaces and prises");
            Console.WriteLine("X. Exit");
            Console.WriteLine("-------------------------");
        }

    public static void BuyParkingSpace(ParkingLot parkingLot)
    {
        Console.Write("Enter license plate: ");
        string licensePlate = Console.ReadLine().ToUpper();
        Console.Write("Define vehicle type: 0. Normal, 1. Handicap, 2. Bus, 3. Motorcycle: ");
        int vehicleType = int.TryParse(Console.ReadLine(), out vehicleType) ? vehicleType : 0;

        ParkingSpaceTypes parkingSpaceTypes = (ParkingSpaceTypes)vehicleType;

        Regex rx = new(@"[A-Z]{2}\s*[0-9]{2}\s*[0-9]{3}");

        if (rx.IsMatch(licensePlate))
        {
            Console.WriteLine(parkingLot.LeaseParkingSpace(parkingSpaceTypes, licensePlate));
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

    public static void BuyCarWash()
    {
    }


}