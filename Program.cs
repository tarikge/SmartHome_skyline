using SmartHome_skyline.Services;

namespace SmartHome_skyline;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Skyline!");
        
        //prikaz pocetnog izbornika za objekte, reu(realestateunit)
        RealEstateUnitService REUservice= new RealEstateUnitService();
        REUservice.displayReuMenu();
    }
}