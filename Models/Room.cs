

using SmartHome_skyline.jsonData;

namespace SmartHome_skyline.Models;

public class Room
{
    public int ID { get; set; }
    public string Name { get; set; }
    public List<Device> Devices { get; set; } = new List<Device>(); 
    

    public Room(int id, string name )
    {
        this.ID = id;
        this.Name = name;
    }


    public void AddDevice(string name, string make, string model, bool status, int type)
    {
        int newId = Devices.Count==0 ? 1: Devices.Max(d => d.ID) + 1;
        switch (type)
        {
            case 1:
                Devices.Add(new AirconditioningDevice(newId, name, make, model, status));
                break;
            case 2:
                Devices.Add(new WashingMachineDevice(newId, name, make, model, status));
                break;
            case 3:
                Devices.Add(new DishwasherDevice(newId, name, make, model, status));
                break;
            case 4:
                Devices.Add(new Device(newId, name, make, model, status));
                break;

        }

    }

    public void RemoveDevice(int id)
    {
        Devices.Remove(Devices.Where(d=>d.ID == id).First());
    }
    public override string ToString()
    {
        return $"{Name}";
    }
    
}