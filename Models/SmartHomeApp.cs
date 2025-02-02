namespace SmartHome_skyline.Models;

public class SmartHomeApp
{
    public List<RealEstateUnit> units {  get; set; }= new List<RealEstateUnit>();
    public User currentUser { get; set; }

    public SmartHomeApp(string username, string password)
    {
    currentUser = new User(username, password);

    }

    public void AddUnit(string name, string location, bool status)
    {
        int newUnitId =units.Count==0 ? 1: units.Max(d => d.ID) + 1;
        units.Add(new RealEstateUnit(newUnitId, name, location, status));
    }

    public void RemoveUnit(int id)
    {
        units.Remove(units.Where(u => u.ID == id).First());
    }

    public void ModifyUnit(int id, string name, string location, bool status)
    {
        units.Where(u=>u.ID == id).First().Name = name;
        units.Where(u => u.ID == id).First().Location = location;
        units.Where(u => u.ID == id).First().Status = status;
        
    }
    
    
}