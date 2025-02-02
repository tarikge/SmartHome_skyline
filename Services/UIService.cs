using System.Threading.Channels;
using Newtonsoft.Json;
using SmartHome_skyline.Models;

namespace SmartHome_skyline.Services;

public class UIService
{
    private SmartHomeApp _smartHomeApp;
    private int state = 0;
    public int selectedUnitId = -1;
    public int selectedRoomId = -1;
    public string username = "";

    public UIService()
    {

    }

    public void displayMenu()
    {
        switch (state)
        {
            case -1:
                Logout();
                state = 0;
                break;

            case 0:
                try
                {
                    state = Login();
                    displayMenu();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                break;
            case 1:
                ListAllUnits();
                mainMenu();
                break;
            case 2:
                showUnitDetails();
                unitMenu();
                break;
            case 3:
                showRoomDetails();
                roomMenu();
                break;
    
        }
    }

    private void roomMenu()
    {
        Console.WriteLine("0. Logout");
        Console.WriteLine("1. Dodaj uredjaj");
        Console.WriteLine("2. Odaberi uredjaj");
        Console.WriteLine("3. Obrisi uredjaj");
        Console.WriteLine("4. Nazad");
        int.TryParse(Console.ReadLine(), out int selection);
        switch (selection)
        {
            case 0:
                state = -1;
                selectedRoomId = -1;
                selectedUnitId = -1;
                break;
            case 1:
                AddDevice();
                SaveData();
                break;
            case 2:
                try
                {
                    DeviceControl();
                    SaveData();

                }
                catch (Exception e)
                {
                    state = 3;
                }

                break;
            case 3:
                try
                {
                    DeleteDevice();
                    SaveData();
                }
                catch (Exception e)
                {
                    state = 3;
                }

                break;
            case 4:
                state = 2;
                selectedRoomId = -1;
                break;
            default:
                Console.WriteLine("Nepostojeca opcija");
                break;
        }

    }

    private void AddDevice()
    {
        Console.WriteLine("Odaberite tip uredjaja");
        Console.WriteLine("(1) Klima uredjaj || (2) Masina za ves || (3) Masina za sudje || (4) Drugi");
        int.TryParse(Console.ReadLine(), out int type);
        Console.WriteLine("Unesite naziv uredjaja:");
        string name = Console.ReadLine();
        Console.WriteLine("Unesite proizvodjaca:");
        string make = Console.ReadLine();
        Console.WriteLine("Unesite model:");
        string model = Console.ReadLine();
        bool boolStatus;

        while (true)
        {
            Console.WriteLine("Unesite status (a/n): ");

            string status = Console.ReadLine();
            if (status == "a")
            {
                boolStatus = true;
                break;
            }
            else if (status == "n")
            {
                boolStatus = false;
                break;
            }
            else Console.WriteLine("Pogresan unos! ");
        }

        var unit = _smartHomeApp.units.Where(u => u.ID == selectedUnitId).FirstOrDefault();
        var room = unit.Rooms.Where(r => r.ID == selectedRoomId).FirstOrDefault();
        room.AddDevice(name, make, model, boolStatus, type);

    }

    private void DeleteDevice()
    {
        var unit = _smartHomeApp.units.Where(u => u.ID == selectedUnitId).FirstOrDefault();
        var room = unit.Rooms.Where(r => r.ID == selectedRoomId).FirstOrDefault();
        Console.WriteLine("Unesite ID uredjaja koji zelite ukloniti");
        int.TryParse(Console.ReadLine(), out int deviceId);
        room.RemoveDevice(deviceId);
    }

    private void DeviceControl()
    {
        Console.WriteLine("Unesite ID uredjaja:");
        int.TryParse(Console.ReadLine(), out int deviceId);
        var unit = _smartHomeApp.units.Where(u => u.ID == selectedUnitId).FirstOrDefault();
        var room = unit.Rooms.Where(r => r.ID == selectedRoomId).FirstOrDefault();
        room.Devices.Where(d => d.ID == deviceId).FirstOrDefault().ControlDevice();
    }

    private void showRoomDetails()
    {
        if (selectedRoomId != -1)
        {
            var unit = _smartHomeApp.units.Where(u => u.ID == selectedUnitId).FirstOrDefault();
            var room = unit.Rooms.Where(r => r.ID == selectedRoomId).FirstOrDefault();
            Console.WriteLine($"ID: {room.ID} -> {room.Name}");
            if (room.Devices.Count == 0)
            {
                Console.WriteLine("Nema uredjaja u ovoj sobi!");
            }

            foreach (var device in room.Devices)
            {
                Console.WriteLine(device.ToString());
            }
        }
    }

    private void showUnitDetails()
    {
        if (selectedUnitId != -1)
        {
            var unit = _smartHomeApp.units.Where(u => u.ID == selectedUnitId).FirstOrDefault();
            Console.WriteLine($"ID: {unit.ID} -> {unit.Name},  {unit.Location}, {unit.Status}");
            if (unit.Rooms.Count == 0)
            {
                Console.WriteLine("Nema soba u ovom objektu!");
            }

            foreach (var room in unit.Rooms)
            {
                Console.WriteLine($"ID-> {room.ID}, Soba: {room.Name}");
                /*if (room.Devices.Count == 0)
                {
                    Console.WriteLine();
                }
                foreach (var device in room.Devices)
                {
                    Console.WriteLine($"ID-> {device.ID}, Uredjaj: {device.Name}, {device.Make}, {device.Model}, {device.GetStatus}");

                }*/
            }

        }


    }

    private void unitMenu()
    {
        Console.WriteLine("Objekat meni");
        Console.WriteLine("0. Logout");
        Console.WriteLine("1. Dodaj sobu");
        Console.WriteLine("2. Odaberi sobu");
        Console.WriteLine("3. Obrisi sobu");
        Console.WriteLine("4. Nazad");
        int.TryParse(Console.ReadLine(), out int selection);

        switch (selection)
        {
            case 0:
                selectedUnitId = -1;
                state = -1;
                break;
            case 1:
                AddRoom();
                SaveData();
                break;
            case 2:
                try
                {
                    SelectRoom();
                    state = 3;
                }
                catch (Exception e)
                {
                    state = 2;
                }

                break;
            case 3:
                try
                {
                    DeleteRoom();
                    SaveData();
                }
                catch (Exception e)
                {
                    state = 2;
                }

                break;
            case 4:
                selectedUnitId = -1;
                state = 1;
                break;
            default:
                Console.WriteLine("Nepostojeca opcija");
                break;
        }
    }

    private void SelectRoom()
    {
        Console.WriteLine("Unesite ID sobe koju zelite da odaberete: ");
        int.TryParse(Console.ReadLine(), out int RoomId);
        selectedRoomId = RoomId;
    }

    private void AddRoom()
    {
        Console.WriteLine("Unesite naziv nove sobe: ");
        string name = Console.ReadLine();
        _smartHomeApp.units.Where(u => u.ID == selectedUnitId).FirstOrDefault().AddRoom(name);
    }

    private void DeleteRoom()
    {
        Console.WriteLine("Unesite ID sobe koju zelite da uklonite: ");
        int.TryParse(Console.ReadLine(), out int RoomId);
        _smartHomeApp.units.Where(u => u.ID == selectedUnitId).FirstOrDefault().RemoveRoom(RoomId);

    }

    private void mainMenu()
    {
        Console.WriteLine("Glavni meni");
        Console.WriteLine("0. Logout");
        Console.WriteLine("1. Dodaj objekat");
        Console.WriteLine("2. Odaberi objekat");
        Console.WriteLine("3. Obrisi objekat");
        int.TryParse(Console.ReadLine(), out int selection);
        switch (selection)
        {
            case 0:
                state = -1;
                break;
            case 1:
                AddUnit();
                state = 1;
                break;
            case 2:
                try
                {
                    SelectUnit();
                    state = 2;
                }
                catch (IndexOutOfRangeException e)
                {
                    state = 1;
                }

                break;
            case 3:
                try
                {
                    DeleteUnit();
                    state = 1;
                }
                catch (IndexOutOfRangeException e)
                {
                    state = 1;
                }

                break;
            default:
                Console.WriteLine("Nepostojeca opcija");
                break;

        }

    }

    private void SelectUnit()
    {
        if (_smartHomeApp.units.Count == 0)
        {
            throw new IndexOutOfRangeException("Nema objekata!");
        }

        Console.WriteLine("Unesite ID objekta koji zelite da odaberete: ");
        int.TryParse(Console.ReadLine(), out int unitID);
        selectedUnitId = unitID;

    }

    private void Logout()
    {
        _smartHomeApp = null;
        selectedRoomId = -1;
        selectedUnitId = -1;
        
    }



    private void ListAllUnits()
    {
        if (_smartHomeApp.units.Count == 0)
        {
            Console.WriteLine("Nema objekata! ");
        }

        foreach (var unit in _smartHomeApp.units)
        {
            Console.WriteLine($"ID: {unit.ID} -> {unit.Name},  {unit.Location}, {unit.Status}");
        }
    }

    private void DeleteUnit()
    {
        if (_smartHomeApp.units.Count == 0)
        {
            throw new IndexOutOfRangeException("Nema objekata!");
        }

        Console.WriteLine("Unesite ID objekta koji zelite da uklonite: ");
        int.TryParse(Console.ReadLine(), out int unitID);
        _smartHomeApp.RemoveUnit(unitID);
    }

    public void AddUnit()
    {
        Console.WriteLine("Unesite ime novog objekta: ");
        string name = Console.ReadLine();
        Console.WriteLine("Unesite lokaciju: ");
        string location = Console.ReadLine();
        bool boolStatus;

        while (true)
        {
            Console.WriteLine("Unesite status (a/n): ");

            string status = Console.ReadLine();
            if (status == "a")
            {
                boolStatus = true;
                break;
            }
            else if (status == "n")
            {
                boolStatus = false;
                break;
            }
            else Console.WriteLine("Pogresan unos! ");
        }

        _smartHomeApp.AddUnit(name, location, boolStatus);
    }

    private int Login()
    {
        Console.WriteLine("Unesite username: ");
        string username = Console.ReadLine();
        Console.WriteLine("Unesite password: ");
        string password = Console.ReadLine();
        return LoadData(username, password);
        
        
    }

    private void SaveData()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../jsonData", "data.json"); 

        List<Data> dataList = new List<Data>();

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            if (!string.IsNullOrWhiteSpace(existingJson))
            {
                dataList = JsonConvert.DeserializeObject<List<Data>>(existingJson) ?? new List<Data>();
            }
        }

        var newData = new Data
        {
            Username = _smartHomeApp.currentUser.Username,
            Password = _smartHomeApp.currentUser.Password,
            RealEstateUnits = _smartHomeApp.units
        };

        var existingData = dataList.FirstOrDefault(d => d.Username == newData.Username && d.Password == newData.Password);

        if (existingData != null)
        {
            existingData.RealEstateUnits = newData.RealEstateUnits;
        }
        else
        {
            dataList.Add(newData);
        }

        string updatedJson = JsonConvert.SerializeObject(dataList, Formatting.Indented);
        File.WriteAllText(filePath, updatedJson);
    
        Console.WriteLine("Uspjesno spaseno.");
    }


    private int LoadData(string username, string password)
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../jsonData", "data.json"); 

        List<Data> dataList = new List<Data>();

        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            if (!string.IsNullOrWhiteSpace(existingJson))
            {
                dataList = JsonConvert.DeserializeObject<List<Data>>(existingJson) ?? new List<Data>();
            }
        }

        var existingUser = dataList.FirstOrDefault(d => d.Username == username);

        if (existingUser != null)
        {
            if (existingUser.Password == password)
            {
                _smartHomeApp = new SmartHomeApp(username, password);

                _smartHomeApp.units = existingUser.RealEstateUnits;
                return 1;
            }
            else
            {
                return -1;

            }
        }
        else
        {
            var newUser = new Data
            {
                Username = username,
                Password = password, 
                RealEstateUnits = new List<RealEstateUnit>()
            };
            _smartHomeApp = new SmartHomeApp(username, password);

            dataList.Add(newUser);
        
            string updatedJson = JsonConvert.SerializeObject(dataList, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);

            Console.WriteLine($"Korisnik {username} nije pronadjen. Novi korisnik kreiran.");
            return 1;
        }
    }

}