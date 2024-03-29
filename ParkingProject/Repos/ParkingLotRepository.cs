using ParkingProject.Models;
using ParkingProject.Data;
using ParkingProject.BLL;

namespace ParkingProject;
    public class ParkingLotRepository : IParkingLotRepository
    {
    public string LeaseParkingSpace(ParkingSpaceTypes parkingSpaceTypes, string licensePlate, List<IParkingSpace> parkingSpaces, bool hasPurchasedCarWash)
    {
        IParkingSpace tempspace;
        tempspace = parkingSpaces.Exists(x => x.LicensePlate == licensePlate) ? parkingSpaces.FirstOrDefault(x => x.LicensePlate == licensePlate) : parkingSpaces.FirstOrDefault(x => x.Type == parkingSpaceTypes && x.IsAvailable);
        if (tempspace != null)
        {
            if (tempspace.IsAvailable == false)
            {
                return "You already parked your car";
            }
            tempspace.IsAvailable = false;
            tempspace.TimeTaken = DateTime.Now;
            tempspace.LicensePlate = licensePlate;
            tempspace.PurchasedCarWash = hasPurchasedCarWash;
            string availTemp = tempspace.PurchasedCarWash ? $"Bought for vehicle: {tempspace.LicensePlate}" : "Not bought";
            return $"Parking space leased for: {tempspace.LicensePlate}\nYour hourly rate is: {tempspace.Price:C}\nYour parking space ID is: {tempspace.Id}\nCarWash: {availTemp}";
        }
        else {
        return "No available parking spaces of that type";
        }
    }

    public string PayLease(string licensePlate, ParkingLot parkingLot) 
    {
        IParkingSpace tempSpace = parkingLot.ParkingSpaceList.FirstOrDefault(x => x.LicensePlate == licensePlate);
        CarWashSpace tempCarWashSpace = parkingLot.CarWash.CarWashSpace.FirstOrDefault(x => x.LicensePlate == licensePlate);
        if (tempSpace == null) return "No such license plate";
        if (tempCarWashSpace != null) return "Car is in car wash";
        if (tempSpace != null && tempCarWashSpace == null) {
        TimeSpan timeTaken = DateTime.Now - (DateTime) tempSpace.TimeTaken;
        double hours = Math.Ceiling(timeTaken.TotalHours);
        double totalCost = hours * tempSpace.Price;
        if (tempSpace.PurchasedCarWash) {
            totalCost += parkingLot.CarWash.Price;
        }
        string returnMessage = $"------ {parkingLot.Name} ------\nParking paid for: {licensePlate}\nHours parked: {hours}\nFee charged: {totalCost:C}\nPaid for car wash: {tempSpace.PurchasedCarWash}\n-------------------------";
        tempSpace.IsAvailable = true; tempSpace.TimeTaken = null; tempSpace.LicensePlate = null; tempSpace.PurchasedCarWash = false; tempSpace.Price = parkingLot.ParkingPrices[(int) tempSpace.Type];
        return returnMessage;
        } else {
        return "Error";
        }
    }

    public void AddParkingSpaces(List<IParkingSpace> parkingSpaces, int NumberOfSpaces, double[] parkingPrices)
    {
        for (int i = 0; i < NumberOfSpaces; i++)
        {
            if (i < 10)
            {
                parkingSpaces.Add(new NormalParkingSpace(i, parkingPrices[0], true));
            }
            else if (i < 14)
            {
                parkingSpaces.Add(new HandicapParkingSpace(i, parkingPrices[1], true));
            }
            else if (i < 16)
            {
                parkingSpaces.Add(new BusParkingSpace(i, parkingPrices[2], true));
            }
            else
            {
                parkingSpaces.Add(new MotorcycleParkingSpace(i, parkingPrices[3], true));
            }
        }
    }
}