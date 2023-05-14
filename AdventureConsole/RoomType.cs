namespace AdventureConsole;

[System.Flags]
public enum RoomType
{
    Default = 0,
    NorthExit = 1,
    EastExit = 2,
    SouthExit = 4,
    WestExit = 8,
    Open = 15,
}