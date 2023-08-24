using ParkingProject.Models;
using ParkingProject.Data;
namespace ParkingProject;
    public class ParkingLotRepository : IParkingLotRepository
    {
        public string LeaseParkingSpace(ParkingSpaceTypes parkingSpaceTypes, string licensePlate, List<IParkingSpace> parkingSpaces, bool hasPurchasedCarWash)
    {
        IParkingSpace tempspace = parkingSpaces.FirstOrDefault(x => x.Type == parkingSpaceTypes && x.IsAvailable);
        if (tempspace != null)
        {
            tempspace.IsAvailable = hasPurchasedCarWash;
            tempspace.TimeTaken = DateTime.Now;
            tempspace.LicensePlate = licensePlate;
            tempspace.PurchasedCarWash = false;
            string availTemp = tempspace.PurchasedCarWash ? "Not bought" : $"Bought for vehicle: {tempspace.LicensePlate}";
            return $"Parking space leased for: {tempspace.LicensePlate}\nYour hourly rate is: {tempspace.Price:C}\nYour parking space ID is: {tempspace.Id}\nCarWash: {availTemp}";
        } else
        {
            return "No available parking spaces of that type";
        }
    }

    public void AddParkingSpaces(List<IParkingSpace> parkingSpaces, int NumberOfSpaces)
    {
        for (int i = 0; i < NumberOfSpaces; i++)
        {
            if (i < 10)
            {
                parkingSpaces.Add(new NormalParkingSpace(i, 12.00, true));
            }
            else if (i < 14)
            {
                parkingSpaces.Add(new HandicapParkingSpace(i, 16.00, true));
            }
            else if (i < 16)
            {
                parkingSpaces.Add(new BusParkingSpace(i, 25.00, true));
            }
            else
            {
                parkingSpaces.Add(new MotorcycleParkingSpace(i, 5.00, true));
            }
        }
    }
}