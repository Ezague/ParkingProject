using Microsoft.Extensions.DependencyInjection;
using ParkingProject.BLL;
using ParkingProject.Models;
using ParkingProject.Data;
using ParkingProject.Repos;

namespace ParkingProject;

internal class Program
{
    static async void Main(string[] args)
    {
        var services = new ServiceCollection()
            .AddSingleton<IParkingLotRepository, ParkingLotRepository>()
            .BuildServiceProvider();

        ParkingLot parkingLot = new(1, new Address("Testvej", "123", 1234, "Danmark"), "TheCarLimbo", services.GetRequiredService<IParkingLotRepository>());
        bool afslut = false;

        while (!afslut)
        {
            Console.Clear();
            ConsoleRepository.PrintMenu(parkingLot);
            ConsoleKeyInfo valg = Console.ReadKey(true);
            switch (valg.Key)
            {
                case ConsoleKey.P:
                    ConsoleRepository.BuyParkingSpace(parkingLot);
                    break;
                case ConsoleKey.B:
                    await ConsoleRepository.WashCarAsync(parkingLot);
                    break;
                case ConsoleKey.S:
                    ConsoleRepository.ShowSpacesAndPrices(parkingLot);
                    break;
                case ConsoleKey.L:
                    ConsoleRepository.PayLease(parkingLot);
                    break;
                case ConsoleKey.A:
                    ConsoleRepository.PrintAdminMenu(parkingLot);
                    break;
                case ConsoleKey.X:
                    afslut = true;
                    break;
            }
        }
    }
}