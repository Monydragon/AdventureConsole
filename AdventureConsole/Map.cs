using System.Numerics;

namespace AdventureConsole;

[System.Serializable]
public static class Map
{
    public static List<Room> Rooms { get; set; } = new();

    public static Room? GetRoom(Vector2 pos)
    {
        return Rooms.Find(x => x.Position == pos)!;
    }

    public static Room? GetRoom(string name)
    {
        return Rooms.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))!;
    }

    public static Room GetRoom(int index)
    {
        return Rooms[index];
    }

    public static bool RoomExists(Vector2 pos)
    {
        return Rooms.Exists(x => x.Position == pos);
    }

    public static bool RoomExists(string name)
    {
        return Rooms.Exists(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }
    
    public static Room AddRoom(string name, Vector2 pos, RoomType type)
    {
        var room = Rooms.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)|| x.Position == pos);
        if (room == null)
        {
            room = new Room(name, pos, type);
            Rooms.Add(room);
            return room;
        }

        return room;
    }
    
    public static Room RemoveRoom(string name)
    {
        var room = Rooms.Find(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        if (room != null)
        {
            Rooms.Remove(room);
            return room;
        }

        return room;
    }

    public static Room RemoveRoom(Vector2 pos)
    {
        var room = Rooms.Find(x => x.Position == pos);
        if (room != null)
        {
            Rooms.Remove(room);
            return room;
        }

        return room;
    }
}