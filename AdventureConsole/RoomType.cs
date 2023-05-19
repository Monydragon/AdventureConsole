namespace AdventureConsole;

[System.Flags]
public enum RoomType
{
    Invalid = -1,
    Default = 0,
    NorthExit = 1,
    EastExit = 2,
    SouthExit = 4,
    WestExit = 8,
    Open = 15,
}