using System.Numerics;

namespace AdventureConsole;

[System.Serializable]
public class Player
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Vector2 Position { get; set; } = Vector2.Zero;
}