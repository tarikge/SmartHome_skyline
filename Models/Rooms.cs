namespace SmartHome_skyline.Models;

public class Rooms
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int UnitID { get; set; }
    public RealEstateUnit Unit { get; set; }
    public Devices[] Devices { get; set; }

    public Rooms(int id, string name, int unitID)
    {
        this.ID = id;
        this.Name = name;
        this.UnitID = unitID;
    }
}