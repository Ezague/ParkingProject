using ParkingProject.Models;
using ParkingProject.BLL;
namespace ParkingProject.Data;

public interface IParkingLotRepository
{
    public string LeaseParkingSpace(ParkingSpaceTypes parkingSpaceTypes, string licensePlate, List<IParkingSpace> parkingSpaces, bool hasPurchasedCarWash);
    public string PayLease(string licensePlate, ParkingLot parkingLot);
    public void AddParkingSpaces(List<IParkingSpace> parkingSpaces, int NumberOfSpaces);
}