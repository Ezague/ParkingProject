using System.Diagnostics.CodeAnalysis;
using ParkingProject.BLL;

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
    public string? LicensePlate { get; set; }

    [SetsRequiredMembers]
    public CarWashSpace(int id, ProcessState state)
{
    Id = id;
    State = state;
}

}
