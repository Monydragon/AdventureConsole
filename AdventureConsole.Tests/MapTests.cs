using System.Diagnostics;
using System.Numerics;
using AdventureConsole;
using NuGet.Frameworks;

namespace AdventureConsole.Tests;

[TestFixture]
public class MapTests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    [TestCase(10)]
    [TestCase(20)]
    [TestCase(50)]
    [TestCase(100)]
    [TestCase(500)]
    [TestCase(1000)]    
    public void CheckRoomAmountGenerated(int numberOfRoomsToGenerate)
    {
        MapGenerator.NumberOfRoomsToGenerate = numberOfRoomsToGenerate;
        MapGenerator.GenerateMap();
        Assert.That(Map.Rooms.Count, Is.EqualTo(numberOfRoomsToGenerate));
    }

    [Test]
    [TestCase(10)]
    [TestCase(20)]
    [TestCase(50)]
    [TestCase(100)]
    [TestCase(500)]
    [TestCase(1000)]
    public void CheckValidRoomDoorsTest(int numberOfRoomsToGenerate)
    {
        MapGenerator.NumberOfRoomsToGenerate = numberOfRoomsToGenerate;
        MapGenerator.GenerateMap();
        
        for (int i = 0; i < Map.Rooms.Count; i++)
        {
            var room = Map.Rooms[i];
            if (room.Type.HasFlag(RoomType.NorthExit))
            {
                var northRoom = Map.GetRoom(room.Position + Vector2.UnitY);
                if(northRoom == null)
                {
                    Assert.Fail("Failed northRoom is null");
                }
                if (!northRoom.Type.HasFlag(RoomType.SouthExit))
                {
                    Assert.Fail("Failed checking north room.");
                }
            }
            if (room.Type.HasFlag(RoomType.EastExit))
            {
                var eastRoom = Map.GetRoom(room.Position + Vector2.UnitX);
                if(eastRoom == null)
                {
                    Assert.Fail("Failed eastRoom is null");
                }
                if (!eastRoom.Type.HasFlag(RoomType.WestExit))
                {
                    Assert.Fail("Failed checking east room.");
                }
            }
            if (room.Type.HasFlag(RoomType.SouthExit))
            {
                var southRoom = Map.GetRoom(room.Position - Vector2.UnitY);
                if(southRoom == null)
                {
                    Assert.Fail("Failed southRoom is null");
                }
                if (!southRoom.Type.HasFlag(RoomType.NorthExit))
                {
                    Assert.Fail("Failed checking south room.");
                }
            }
            if (room.Type.HasFlag(RoomType.WestExit))
            {
                var westRoom = Map.GetRoom(room.Position - Vector2.UnitX);
                if(westRoom == null)
                {
                    Assert.Fail("Failed westRoom is null");
                }
                if (!westRoom.Type.HasFlag(RoomType.EastExit))
                {
                    Assert.Fail("Failed checking west room.");
                }
            }
        }
        Assert.Pass();
    }
}