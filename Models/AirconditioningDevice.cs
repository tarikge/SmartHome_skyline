using SmartHome_skyline.Models;

namespace SmartHome_skyline.jsonData;

public class AirconditioningDevice : Device
{
    public float Temperature { get; set; } = 25;
    public int FanSpeed { get; set; } = 1;
    public int Mode { get; set; } = 1;

    public AirconditioningDevice(int id, string name , string make, string model, bool status):base(id,name,make,model,status )
    {
        
    }
    public override string GetStatus()
    {
        if(Status==true) return "On";
        else return "Off";
        }
    public override void ControlDevice()
    {
        string statustxt = GetStatus();
        Console.WriteLine($"Trenutni status:{statustxt}");
        Console.WriteLine($"Temperatura status:{Temperature}");
        Console.WriteLine($"Fan speed:{FanSpeed}");
        Console.WriteLine($"Nacin rada:{Mode}");
        Console.WriteLine("Promjeni status 0-off 1-on");
        int.TryParse(Console.ReadLine(), out int statusChanged);
        if(statusChanged==0) this.Status = false;
        else this.Status = true;
        Console.WriteLine("Unesite zeljenu temperaturu:");
        int.TryParse(Console.ReadLine(), out int temperatureChanged);
        Console.WriteLine("Unesite zeljenu brzinu:");
        int.TryParse(Console.ReadLine(), out int fanSpeecChanged);
        Console.WriteLine("Unesite zeljeni nacin rada 0-hladjenje, 1-grijanje");
        int.TryParse(Console.ReadLine(), out int modeChanged);
        this.Temperature = temperatureChanged;
        this.FanSpeed = fanSpeecChanged;
        this.Mode = modeChanged;
        
    }
    public override string ToString()
    {
        return $"Uredjaj: ID: {ID} ->{Name} - {Make} - {Model} \n Status: {GetStatus()} \n Temperature: {Temperature} \n Fan speed: {FanSpeed} \n Mode: {Mode}";
    }
}