using System.Diagnostics.CodeAnalysis;
using ParkingProject.Models;
using ParkingProject.Data;

namespace ParkingProject.BLL;

public class ParkingLot
{
    public required int Id { get; set; }
    public required Address Address { get; set; }
    public required string Name { get; set; }
    public required int NumberOfSpaces { get; set; }
    public List<IParkingSpace> ParkingSpaceList { get { return _parkingSpaces; }}
    public CarWash CarWash { get; init; }
    public readonly IParkingLotRepository ParkingLotRepository;

    #region backing fields
    private List<IParkingSpace> _parkingSpaces;
    #endregion

    [SetsRequiredMembers]
    public ParkingLot(int id, Address address, string name, IParkingLotRepository parkingLotRepository)
    {
        Id = id;
        Address = address;
        Name = name;
        ParkingLotRepository = parkingLotRepository;
        NumberOfSpaces = 20;
        _parkingSpaces = new List<IParkingSpace>();
        ParkingLotRepository.AddParkingSpaces(_parkingSpaces, NumberOfSpaces);
        CarWash = new(1, "Wash if you're gay");
    }

    public string LeaseParkingSpace(ParkingSpaceTypes parkingSpaceTypes, string licensePlate)
    {
        return ParkingLotRepository.LeaseParkingSpace(parkingSpaceTypes, licensePlate, _parkingSpaces);
    }
}