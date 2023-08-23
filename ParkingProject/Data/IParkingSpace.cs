using ParkingProject.Models;

namespace ParkingProject.Data;

public interface IParkingSpace
{
    public int Id { get; set; }
    public ParkingSpaceTypes Type { get; set; }
    public double Price { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime? TimeTaken { get; set; }
    public bool PurchasedCarWash { get; set; }
    public string? LicensePlate { get; set; }
}