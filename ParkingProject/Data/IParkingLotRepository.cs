using ParkingProject.Models;
namespace ParkingProject.Data;

public interface IParkingLotRepository
{
    public string LeaseParkingSpace(ParkingSpaceTypes parkingSpaceTypes, string licensePlate, List<IParkingSpace> parkingSpaces);
    public void AddParkingSpaces(List<IParkingSpace> parkingSpaces, int NumberOfSpaces);
}