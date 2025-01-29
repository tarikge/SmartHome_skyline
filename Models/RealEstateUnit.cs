namespace SmartHome_skyline.Models;

public class RealEstateUnit
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public bool Status { get; set; }

    public RealEstateUnit(int id, string name, string location, bool status)
    {
        this.ID = id;
        this.Name = name;
        this.Location = location;
        this.Status = status;
    }
}