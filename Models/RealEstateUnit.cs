namespace SmartHome_skyline.Models;

public class RealEstateUnit
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public bool Status { get; set; }
    
    public List<Room> Rooms { get; set; } = new List<Room>();

    public RealEstateUnit(int id, string name, string location, bool status)
    {
        this.ID = id;
        this.Name = name;
        this.Location = location;
        this.Status = status;
    }

    public override string ToString()
    {
        string statusText = this.Status ? "Aktivan" : "Neaktivan";

        return $"ID-> {ID}, {Name} - Lokacija: {Location} - Status: {statusText}";
    }

    public void AddRoom(string name)
    {
        int newID = Rooms.Count==0 ? 1: Rooms.Max(d => d.ID) + 1;
        Room room=new Room(newID, name);
        this.Rooms.Add(room);
    }

    public void RemoveRoom(int id)
    {
        this.Rooms.Remove(Rooms.Where(r => r.ID == id).First());

    }
}