namespace ParkingProject.Models;

public enum ProcessStates
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
    public required ProcessStates State { get; set; }
    public required decimal Price { get; set; }
}
