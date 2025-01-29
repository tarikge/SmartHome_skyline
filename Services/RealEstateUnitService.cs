using System.Text.Json;
using System.Text.Json.Nodes;
using SmartHome_skyline.Models;

namespace SmartHome_skyline.Services;

class RealEstateUnitService
{
    private static string FilePath = Path.GetFullPath("jsonData/RealEstateUnits.json");
    private readonly List<RealEstateUnit> _realEstateUnits;

    public RealEstateUnitService()
    {
        _realEstateUnits = LoadUnits();
    }

    private List<RealEstateUnit> LoadUnits()
    {
        if (!File.Exists(FilePath))
        {
            return new List<RealEstateUnit>();
        }

        string jsonData = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<RealEstateUnit>>(jsonData) ?? new List<RealEstateUnit>();
    }

    private void SaveUnits()
    {
        string jsonData =
            JsonSerializer.Serialize(_realEstateUnits, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, jsonData);
    }

    public void AddUnit()
    {
        int newID = _realEstateUnits.Count + 1;
        bool Status;
        Console.WriteLine("Unesite naziv objekta: ");
        string Name = Console.ReadLine();

        Console.WriteLine("Unesite lokaciju: ");
        string Location = Console.ReadLine();

        while (true)
        {
            Console.WriteLine("Unesite status (A-aktivno, N-neaktivno) : ");
            string text = Console.ReadLine()?.Trim().ToUpper();
            if (text == "A")
            {
                Status = true;
                break;
            }
            else if (text == "N")
            {
                Status = false;
                break;
            }
            else
            {
                Console.WriteLine("Neispravan unos, molimo unesite A za aktivno ili N za neaktivno!");
            }
        }


        RealEstateUnit unit = new RealEstateUnit(newID, Name, Location, Status);
        _realEstateUnits.Add(unit);
        SaveUnits();

        Console.WriteLine($"Objekat imena: {Name} je uspjesno spasen!");

    }

    public void ListAllUnits()
    {
        if (_realEstateUnits.Count == 0)
        {
            Console.WriteLine("Nema objekata!");
            return;
        }

        foreach (var unit in _realEstateUnits)
        {
            string statusText = unit.Status ? "Aktivan" : "Neaktivan";
            Console.WriteLine($"ID:{unit.ID} - {unit.Name}, {unit.Location}, {statusText} ", Environment.NewLine);
        }
        
        Console.WriteLine("Unesite ID objekta kojeg zelite odabrati: ");
        int.TryParse(Console.ReadLine(),out int selectedID);

        if (selectedID > 0 && selectedID < _realEstateUnits.Count)
        {
            var selectedUnit = _realEstateUnits.Where(x => x.ID == selectedID);
            //rooms service .display rooms
        }
    }

    public void DeleteUnit()
    {
        
        if (_realEstateUnits.Count == 0){
            Console.WriteLine("Nema objekata!");
            return;
        }
        Console.WriteLine("Unesite ime objekta koji zelite da uklonite.");
        string name = Console.ReadLine();
        if(_realEstateUnits.Remove(_realEstateUnits.FirstOrDefault(u => u.Name.ToUpper() == name.ToUpper()))){
        SaveUnits();
        Console.WriteLine($"Uspjesno uklonjen objekat naziva {name}!");
        }
        else
        {
            Console.WriteLine($"Objekat sa nazivom {name} nije moguce pronaci!");
        }
}


    public void displayReuMenu()
    {
        while (true)
        {
            Console.WriteLine("\nIzbornik: ");
            Console.WriteLine("1. Dodaj objekat: ");
            Console.WriteLine("2. Ispisi objekte: ");//selekciju treba uradit -> prostorije tog objekta
            Console.WriteLine("3. Obrisi objekat: ");
            
            Console.WriteLine("Odaberite opciju: ");
            int selection = int.Parse(Console.ReadLine());

            switch (selection)
            {
                case 1:
                    AddUnit();
                    break;
                case 2:
                    ListAllUnits();
                    break;
                case 3:
                    DeleteUnit();
                    break;
            }

        }
    }

}