using System.Diagnostics.CodeAnalysis;
using ParkingProject.Models;
using ParkingProject.Data;

namespace ParkingProject.BLL;

public class ParkingLot
{
    public required int Id { get; set; }
    public required Address Address { get; set; }
    public required string Name { get; set; }
    public double[] ParkingPrices { get; set; }
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
        ParkingPrices = new double[] { 12.00, 16.00, 25.00, 5.00 };
        ParkingLotRepository = parkingLotRepository;
        NumberOfSpaces = 20;
        _parkingSpaces = new List<IParkingSpace>();
        ParkingLotRepository.AddParkingSpaces(_parkingSpaces, NumberOfSpaces, ParkingPrices);
        CarWash = new(1, "Washer", 79);
    }
}
