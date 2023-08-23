using System.Diagnostics.CodeAnalysis;
using ParkingProject.Data;
namespace ParkingProject.Models;

public enum ParkingSpaceTypes
{
    Normal,
    Handicap,
    Bus,
    Motorcycle
}

internal abstract class ParkingSpace : IParkingSpace
{
    
    public required int Id { get; set; }
    public required ParkingSpaceTypes Type { get; set; }
    public required double Price { get; set; }
    public required bool IsAvailable { get; set; }
    public DateTime? TimeTaken { get; set; }
    public bool PurchasedCarWash { get; set; }
    public string? LicensePlate { get; set; }

    [SetsRequiredMembers]
    public ParkingSpace(int id,ParkingSpaceTypes type, double price, bool isAvailable)
    {
        Id = id;
        Type = type;
        Price = price;
        IsAvailable = isAvailable;
    }
}

internal class NormalParkingSpace : ParkingSpace
{
    [SetsRequiredMembers]
    public NormalParkingSpace(int id, double price, bool isAvailable) : base(id, ParkingSpaceTypes.Normal, price, isAvailable)
    {
        Id = id;
        Price = price;
        IsAvailable = isAvailable;
    }
}

internal class HandicapParkingSpace : ParkingSpace
{
    [SetsRequiredMembers]
    public HandicapParkingSpace(int id, double price, bool isAvailable) : base(id, ParkingSpaceTypes.Handicap, price, isAvailable)
    {
        Id = id;
        Price = price;
        IsAvailable = isAvailable;
    }
}

internal class BusParkingSpace : ParkingSpace
{
    [SetsRequiredMembers]
    public BusParkingSpace(int id, double price, bool isAvailable) : base(id, ParkingSpaceTypes.Bus, price, isAvailable)
    {
        Id = id;
        Price = price;
        IsAvailable = isAvailable;
    }
}

internal class MotorcycleParkingSpace : ParkingSpace
{
    [SetsRequiredMembers]
    public MotorcycleParkingSpace(int id, double price, bool isAvailable) : base(id, ParkingSpaceTypes.Motorcycle, price, isAvailable)
    {
        Id = id;
        Price = price;
        IsAvailable = isAvailable;
    }
}