using System.Numerics;

namespace AdventureConsole;

public static class MapGenerator
{
    public static int NumberOfRoomsToGenerate { get; set; } = 10;
    public static Queue<Action> RoomGenerationQueue = new();
    private static HashSet<Vector2> GeneratingPositionsSet = new();

    public static Room GenerateRoom(Vector2 pos, RoomType type, RoomType outputRoomType = RoomType.Invalid)
    {
        Room? rm = null;
        var rnd = new Random();
        var roomTypeVal = (RoomType)rnd.Next((int)RoomType.NorthExit, (int)RoomType.Open + 1);
        var roomName = $"Room {Map.Rooms.Count + 1}";

        if (outputRoomType != RoomType.Invalid)
        {
            roomTypeVal = outputRoomType;

            if (!Map.RoomExists(pos))
            {
                var mainRoom = Map.AddRoom(roomName, pos, type);
                NumberOfRoomsToGenerate--;
            }

            if(outputRoomType.HasFlag(RoomType.NorthExit))
            {
                var newPos = new Vector2(pos.X, pos.Y + 1);
                if (GeneratingPositionsSet.Contains(pos))
                {
                    GeneratingPositionsSet.Remove(pos);
                }
                if(!Map.RoomExists(newPos) && !GeneratingPositionsSet.Contains(newPos))
                {
                    GeneratingPositionsSet.Add(newPos);
                    RoomGenerationQueue.Enqueue(() =>
                    {
                        var newRoomName = $"Room {Map.Rooms.Count + 1}";
                        rm = Map.AddRoom(newRoomName, newPos, RoomType.SouthExit);
                    });
                }
            }
            if (outputRoomType.HasFlag(RoomType.EastExit))
            {
                var newPos = new Vector2(pos.X + 1, pos.Y);
                if (GeneratingPositionsSet.Contains(pos))
                {
                    GeneratingPositionsSet.Remove(pos);
                }
                if (!Map.RoomExists(newPos) && !GeneratingPositionsSet.Contains(newPos))
                {
                    GeneratingPositionsSet.Add(newPos);
                    RoomGenerationQueue.Enqueue(() =>
                    {
                        var newRoomName = $"Room {Map.Rooms.Count + 1}";
                        rm = Map.AddRoom(newRoomName, newPos, RoomType.WestExit);
                    });
                }
            }
            if (outputRoomType.HasFlag(RoomType.SouthExit))
            {
                var newPos = new Vector2(pos.X, pos.Y - 1);
                if (GeneratingPositionsSet.Contains(pos))
                {
                    GeneratingPositionsSet.Remove(pos);
                }
                if (!Map.RoomExists(newPos) && !GeneratingPositionsSet.Contains(newPos))
                {
                    GeneratingPositionsSet.Add(newPos);
                    RoomGenerationQueue.Enqueue(() =>
                    {
                        var newRoomName = $"Room {Map.Rooms.Count + 1}";
                        rm = Map.AddRoom(newRoomName, newPos, RoomType.NorthExit);
                    });
                }
            }
            if (outputRoomType.HasFlag(RoomType.WestExit))
            {
                var newPos = new Vector2(pos.X -1, pos.Y);
                if (GeneratingPositionsSet.Contains(pos))
                {
                    GeneratingPositionsSet.Remove(pos);
                }
                if (!Map.RoomExists(newPos) && !GeneratingPositionsSet.Contains(newPos))
                {
                    GeneratingPositionsSet.Add(newPos);
                    RoomGenerationQueue.Enqueue(() =>
                    {
                        var newRoomName = $"Room {Map.Rooms.Count + 1}";
                        rm = Map.AddRoom(newRoomName, newPos, RoomType.EastExit);
                    });
                }
            }
            NumberOfRoomsToGenerate--;
        }
        else
        {
            if (NumberOfRoomsToGenerate <= 0)
            {
                if (type.HasFlag(RoomType.NorthExit))
                {
                    var northRoom = Map.GetRoom(pos + Vector2.UnitY);
                    if (northRoom.Type.HasFlag(RoomType.SouthExit))
                    {
                        northRoom.Type -= RoomType.SouthExit;
                    }
                }
                if (type.HasFlag(RoomType.EastExit))
                {
                    var eastRoom = Map.GetRoom(pos + Vector2.UnitX);
                    if (eastRoom.Type.HasFlag(RoomType.WestExit))
                    {
                        eastRoom.Type -= RoomType.WestExit;
                    }
                }
                if (type.HasFlag(RoomType.SouthExit))
                {
                    var southRoom = Map.GetRoom(pos - Vector2.UnitY);
                    if (southRoom.Type.HasFlag(RoomType.NorthExit))
                    {
                        southRoom.Type -= RoomType.SouthExit;
                    }
                }
                if (type.HasFlag(RoomType.WestExit))
                {
                    var westRoom = Map.GetRoom(pos - Vector2.UnitX);
                    if (westRoom.Type.HasFlag(RoomType.EastExit))
                    {
                        westRoom.Type -= RoomType.EastExit;
                    }
                }

                return null!;
            }
            GeneratingPositionsSet.Remove(pos);

            if (roomTypeVal.HasFlag(RoomType.NorthExit))
            {
                var newPos = new Vector2(pos.X, pos.Y + 1);
                if (!Map.RoomExists(newPos) && !GeneratingPositionsSet.Contains(newPos))
                {
                    GeneratingPositionsSet.Add(newPos);
                    RoomGenerationQueue.Enqueue(() =>
                    {
                        GenerateRoom(newPos, RoomType.SouthExit);
                    });
                }
                else
                {
                    roomTypeVal -= RoomType.NorthExit;
                }
            }
            if (roomTypeVal.HasFlag(RoomType.EastExit))
            {
                var newPos = new Vector2(pos.X + 1, pos.Y);
                if (!Map.RoomExists(newPos) && !GeneratingPositionsSet.Contains(newPos))
                {
                    GeneratingPositionsSet.Add(newPos);
                    RoomGenerationQueue.Enqueue(() =>
                    {
                        GenerateRoom(newPos, RoomType.WestExit);
                    });
                }
                else
                {
                    roomTypeVal -= RoomType.EastExit;
                }
            }
            if (roomTypeVal.HasFlag(RoomType.SouthExit))
            {
                var newPos = new Vector2(pos.X, pos.Y - 1);
                if (!Map.RoomExists(newPos) && !GeneratingPositionsSet.Contains(newPos))
                {
                    GeneratingPositionsSet.Add(newPos);
                    RoomGenerationQueue.Enqueue(() =>
                    {
                        GenerateRoom(newPos, RoomType.NorthExit);
                    });
                }
                else
                {
                    roomTypeVal -= RoomType.SouthExit;
                }
            }
            if (roomTypeVal.HasFlag(RoomType.WestExit))
            {
                var newPos = new Vector2(pos.X - 1, pos.Y);
                if (!Map.RoomExists(newPos) && !GeneratingPositionsSet.Contains(newPos))
                {
                    GeneratingPositionsSet.Add(newPos);
                    RoomGenerationQueue.Enqueue(() =>
                    {
                        GenerateRoom(newPos, RoomType.EastExit);
                    });
                }
                else
                {
                    roomTypeVal -= RoomType.WestExit;
                }
            }
            roomTypeVal |= type;
            rm = Map.AddRoom(roomName, pos, roomTypeVal);
            NumberOfRoomsToGenerate--;
        }
        return rm;

    }

    public static bool GenerateMap(RoomType defaultInputRoomType = RoomType.Open, RoomType defualtOutputRoomType = RoomType.Invalid)
    {
        if(defualtOutputRoomType != RoomType.Invalid)
        {
            RoomGenerationQueue.Enqueue(() =>
            {
                GenerateRoom(Vector2.Zero, defaultInputRoomType, defualtOutputRoomType);
            });
        }
        else
        {
            RoomGenerationQueue.Enqueue(() =>
            {
                GenerateRoom(Vector2.Zero, RoomType.Default);
            });
        }

        while (RoomGenerationQueue?.Count > 0 || NumberOfRoomsToGenerate > 0)
        {
            if (RoomGenerationQueue?.Count > 0)
            {
                var room = RoomGenerationQueue.Dequeue();
                room.Invoke();
            }
            else
            {
                var rnd = new Random();
                var randomRoomVaL = rnd.Next(0, Map.Rooms.Count);
                var randomRoom = Map.GetRoom(randomRoomVaL);
                var roomType = randomRoom.Type;
                var newPosBase = new Vector2(randomRoom.Position.X, randomRoom.Position.Y);
                if (!roomType.HasFlag(RoomType.NorthExit) && !Map.RoomExists(newPosBase + Vector2.UnitY) && !GeneratingPositionsSet.Contains(newPosBase + Vector2.UnitY))
                {
                    randomRoom.Type |= RoomType.NorthExit;
                    GeneratingPositionsSet.Add(newPosBase + Vector2.UnitY);
                    RoomGenerationQueue.Enqueue(() =>
                    {
                        GenerateRoom(newPosBase + Vector2.UnitY, RoomType.SouthExit);
                    });
                }
                else if (!roomType.HasFlag(RoomType.EastExit) && !Map.RoomExists(newPosBase + Vector2.UnitX) && !GeneratingPositionsSet.Contains(newPosBase + Vector2.UnitX))
                {
                    randomRoom.Type |= RoomType.EastExit;
                    GeneratingPositionsSet.Add(newPosBase + Vector2.UnitX);
                    RoomGenerationQueue.Enqueue(() =>
                    {
                        GenerateRoom(newPosBase + Vector2.UnitX, RoomType.WestExit);
                    });
                }
                else if (!roomType.HasFlag(RoomType.SouthExit) && !Map.RoomExists(newPosBase - Vector2.UnitY) && !GeneratingPositionsSet.Contains(newPosBase - Vector2.UnitY))
                {
                    randomRoom.Type |= RoomType.SouthExit;
                    GeneratingPositionsSet.Add(newPosBase - Vector2.UnitY);
                    RoomGenerationQueue.Enqueue(() =>
                    {
                        GenerateRoom(newPosBase - Vector2.UnitY, RoomType.NorthExit);
                    });
                }
                else if (!roomType.HasFlag(RoomType.WestExit) && !Map.RoomExists(newPosBase - Vector2.UnitX) && !GeneratingPositionsSet.Contains(newPosBase - Vector2.UnitX))
                {
                    randomRoom.Type |= RoomType.WestExit;
                    GeneratingPositionsSet.Add(newPosBase - Vector2.UnitX);
                    RoomGenerationQueue.Enqueue(() =>
                    {
                        GenerateRoom(newPosBase - Vector2.UnitX, RoomType.EastExit);
                    });
                }
            }
            
        }

        return true;
    }
}