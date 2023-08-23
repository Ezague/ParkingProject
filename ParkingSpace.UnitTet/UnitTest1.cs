using Microsoft.Extensions.DependencyInjection;
using ParkingProject;
using ParkingProject.BLL;
using ParkingProject.Data;
using ParkingProject.Models;

namespace ParkingSpace.UnitTet;

public class UnitTest1
{
    [Fact]
    public void PurchaseParkingPass()
    {
        //Arrange
        var services = new ServiceCollection()
            .AddSingleton<IParkingLotRepository, ParkingLotRepository>()
            .BuildServiceProvider();

        string licensePlate = "AA11111";
        ParkingSpaceTypes type = ParkingSpaceTypes.Normal;

        ParkingLot parkingLot = new(1, new Address("Testvej", "123", 1234, "Danmark"), "TheCarLimbo", services.GetRequiredService<IParkingLotRepository>());
        IParkingSpace parkingSpace = parkingLot.ParkingSpaceList.Find(x => x.Type == type && x.IsAvailable);

        //Act
        string result = parkingLot.LeaseParkingSpace(type, licensePlate);

        //Assert
        Assert.Equal($"Parking space leased for: {licensePlate}\nYour hourly rate is: {parkingSpace.Price:C}\nYour parking space ID is: {parkingSpace.Id}", result);
    }
}
