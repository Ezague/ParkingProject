using System.Diagnostics.CodeAnalysis;
using ParkingProject.Models;
namespace ParkingProject.BLL;

public class CarWash
{
    #region properties
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int NumberOfSpaces { get; set; }
    public List<CarWashSpace> CarWashSpace { get { return _carWashSpaces; } }
    #endregion

    #region backing fields
    private List<CarWashSpace> _carWashSpaces;
    #endregion
    [SetsRequiredMembers]
    public CarWash(int id, string name)
    {
        Id = id;
        Name = name;
        NumberOfSpaces = 3;
        _carWashSpaces = new List<CarWashSpace>();
    }

    public void WashCar()
    {
        throw new NotImplementedException();
    }
}
