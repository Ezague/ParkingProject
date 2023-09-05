using System.Diagnostics.CodeAnalysis;
using ParkingProject.Models;
namespace ParkingProject.BLL;

public class CarWash
{
    #region properties
    public required int Id { get; set; }
    public double Price { get; set; }
    public required string Name { get; set; }
    public required int NumberOfSpaces { get; set; }
    public List<CarWashSpace> CarWashSpace { get { return _carWashSpaces; } }
    #endregion

    #region backing fields
    private List<CarWashSpace> _carWashSpaces;
    #endregion
    [SetsRequiredMembers]
    public CarWash(int id, string name, double price)
    {
        Id = id;
        Name = name;
        NumberOfSpaces = 3;
        Price = price;
        _carWashSpaces = new List<CarWashSpace>();
        AddCarWashSpaces(_carWashSpaces);
    }

    public void AddCarWashSpaces(List<CarWashSpace> carWashSpaces)
    {
        for (int i = 0; i < NumberOfSpaces; i++)
        {
            carWashSpaces.Add(new CarWashSpace(i, ProcessState.Free));
        }
    }

    public async Task WashCarAsync(string licensePlate)
    {
        CarWashSpace tempSpace = _carWashSpaces.FirstOrDefault(x => x.State == ProcessState.Free);

        if (tempSpace == null)
        {
            await Console.Out.WriteLineAsync("No free car wash spaces");
            Console.ReadKey();
        }
        else
        {
            tempSpace.LicensePlate = licensePlate;
            foreach (ProcessState state in Enum.GetValues(typeof(ProcessState)))
            {
                if (state != ProcessState.Free)
                {
                    tempSpace.State = state;
                    await Task.Delay(2000);
                    await Console.Out.WriteLineAsync($"Car wash space {tempSpace.Id} is now {tempSpace.State} for {licensePlate}");
                }
            }
            await Task.Delay(2000);
            tempSpace.State = ProcessState.Free;
            await Console.Out.WriteLineAsync($"Car wash space {tempSpace.Id} is now {tempSpace.State}");
            tempSpace.LicensePlate = null;
        }

    }

    public string AdjustWashingPrice(double price)
    {
        Price = price;
        return $"Car wash price is now {Price:C}";
    }

}
