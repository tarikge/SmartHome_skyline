namespace SmartHome_skyline.Models;

public class WashingMachineDevice : Device
{
    public string Capacity { get; set; }
    public string Mode { get; set; }

    
        
    public WashingMachineDevice(int id, string name, string make, string model, bool status):base(id,name,make,model,status )
    {
        
    }
    public override string GetStatus()
    {
        if(Status==true) return "On";
        else return "Off";

    }
}