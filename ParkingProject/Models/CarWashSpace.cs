using System.Diagnostics.CodeAnalysis;
namespace ParkingProject.Models;

public enum ProcessState
{
    Free,
    Prewash,
    Agitate,
    Rinse,
    Wax,
    Dry
}

public class CarWashSpace
{
    public required int Id { get; set; }
    public required ProcessState State { get; set; }
    public required decimal Price { get; set; }

    [SetsRequiredMembers]
    public CarWashSpace(int id, ProcessState state, decimal price)
{
    Id = id;
    State = state;
    Price = price;
}

}
