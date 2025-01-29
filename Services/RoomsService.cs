using SmartHome_skyline.Models;

namespace SmartHome_skyline.Services;

public class RoomsService
{
    private static string FilePath = Path.GetFullPath("jsonData/Rooms.json");
    private readonly List<Rooms> _rooms;

    public RoomsService()
    {
        //_rooms = LoadRooms();
    }
}