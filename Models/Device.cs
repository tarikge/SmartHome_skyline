namespace SmartHome_skyline.Models;

public class Device
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public bool Status { get; set; }


    public Device(int id, string name, string make, string model, bool status)
    {
        this.ID = id;
        this.Name = name;
        this.Make = make;
        this.Model = model;
        this.Status = status;
        
    }
    public virtual string GetStatus()
    {
        string StatusText;
        if (Status)
        {
            StatusText = "On";
            return StatusText;

        }else if (!Status)
        {
            StatusText = "Off";     
            return StatusText;

            
        }

        return "";
    }

    public virtual void ControlDevice()
    {
        string statustxt = GetStatus();
        Console.WriteLine($"Trenutni status:{statustxt}");
        Console.WriteLine("Promjeni status 0-off 1-on");
        int.TryParse(Console.ReadLine(), out int statusChanged);
        if(statusChanged==0) this.Status = false;
        else this.Status = true;
    }
    public override string ToString()
    {
        return $"Uredjaj: ID: {ID} -> {Make} - {Model} \n Status: {GetStatus()}";
    }
}