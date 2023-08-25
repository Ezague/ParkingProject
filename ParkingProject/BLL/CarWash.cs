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
        AddCarWashSpaces(_carWashSpaces);
    }

    public void AddCarWashSpaces(List<CarWashSpace> carWashSpaces)
    {
        for (int i = 0; i < NumberOfSpaces; i++)
        {
            carWashSpaces.Add(new CarWashSpace(i, ProcessState.Free, 79));
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
            foreach (ProcessState state in Enum.GetValues(typeof(ProcessState)))
            {
                if (state != ProcessState.Free)
                {
                    tempSpace.State = state;
                    Task.Delay(2000).Wait();
                    Console.Out.WriteLineAsync($"Car wash space {tempSpace.Id} is now {tempSpace.State} for {licensePlate}");
                }
            }
            Task.Delay(2000).Wait();
            tempSpace.State = ProcessState.Free;
            Console.Out.WriteLineAsync($"Car wash space {tempSpace.Id} is now {tempSpace.State}");
        }

    }
}
