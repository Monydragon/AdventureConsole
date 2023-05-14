using System.Numerics;

namespace AdventureConsole;

[System.Serializable]
public class Room
{
    public string Name { get; set; }
    public Vector2 Position { get; set; }
    public RoomType Type {get; set;}

    public Room(string name, Vector2 position, RoomType type)
    {
        Name = name;
        Position = position;
        Type = type;
    }
}