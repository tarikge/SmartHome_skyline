using SmartHome_skyline.Services;

namespace SmartHome_skyline;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Skyline!");
        
        UIService service= new UIService();
        while (true)
        {
            service.displayMenu();
        }
        
    }
}