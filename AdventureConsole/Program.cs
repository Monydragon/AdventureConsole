using System.Numerics;
using AdventureConsole;

var player = new Player();
MapGenerator.NumberOfRoomsToGenerate = 6;
MapGenerator.GenerateMap();
Console.WriteLine($"Rooms Count {Map.Rooms.Count}");
// var name = GetInput("What is your name?");
// var aliva = GetInputGeneric <bool>("Are you alive?");
// var pref = GetInput("Do you like Dogs or Cats?", "dogs", "dog", "cats", "cat");
// var age = GetInputGeneric<int>("What is your age?");
// var cash = GetInputGeneric<decimal>("How much money do you have?");
// var selection = GetInputGeneric<int>("Select one of the following:\n1: New Player\n2: Existing PLayer", 1, 2);

// var d20Rolls = ConsoleHelper.RollDiceGetRolls(6, 5);
// var top3Rolls = d20Rolls.rolls.OrderDescending().Take(3);
// var sum = top3Rolls.Sum(x => x);
// Console.WriteLine($"You rolled {d20Rolls.rolls.Count}D6 dice: {d20Rolls.total} top {top3Rolls.Count()} sum: {sum}");
string roomOptions = string.Empty;
do
{
    var currentRoom = Map.GetRoom(player.Position);
    Direction options = Direction.None;
    Console.WriteLine($"You enter the room {currentRoom?.Name} at Pos: X: {currentRoom?.Position.X}, Y: {currentRoom?.Position.Y}");
    roomOptions = ConsoleHelper.GetInput($"What would you like to do in this room ({currentRoom?.Name})? (Search, Move, Quit)");

    if (roomOptions.Equals("move", StringComparison.InvariantCultureIgnoreCase))
    {
        switch ((int)currentRoom.Type)
        {
            case 1:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (North)", Direction.N);
                break;
            case 2:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (East)", Direction.E);
                break;
            case 3:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (North, East)",
                    Direction.N, Direction.E);
                break;
            case 4:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (South)", Direction.S);
                break;
            case 5:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (North, South)",
                    Direction.N, Direction.S);
                break;
            case 6:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (East, South)",
                    Direction.E, Direction.S);
                break;
            case 7:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (North, East, South)",
                    Direction.N, Direction.E, Direction.S);
                break;
            case 8:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (West)", Direction.W);
                break;
            case 9:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (North, West)",
                    Direction.N, Direction.W);
                break;
            case 10:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (East, West)", Direction.E,
                    Direction.W);
                break;
            case 11:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (North, East, West)",
                    Direction.N, Direction.E, Direction.W);
                break;
            case 12:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (South, West)",
                    Direction.S, Direction.W);
                break;
            case 13:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (North, South, East)",
                    Direction.N, Direction.S, Direction.E);
                break;
            case 14:
                options = ConsoleHelper.GetInputGeneric("What direction do you want to move? (East, South, West)",
                    Direction.E, Direction.S, Direction.W);
                break;
            case 15:
                options = ConsoleHelper.GetInputGeneric(
                    "What direction do you want to move? (North, East, South, West)", Direction.N, Direction.E,
                    Direction.S, Direction.W);
                break;
        }

        switch (options)
        {
            case Direction.None:
                break;
            case Direction.Up:
                player.Position += Vector2.UnitY;
                Console.WriteLine("You move North");
                break;
            case Direction.Down:
                player.Position -= Vector2.UnitY;
                Console.WriteLine("You move South");
                break;
            case Direction.Right:
                player.Position += Vector2.UnitX;
                Console.WriteLine("You move East");
                break;
            case Direction.Left:
                player.Position -= Vector2.UnitX;
                Console.WriteLine("You move West");
                break;
        }
    }
} while (!roomOptions.Equals("Quit", StringComparison.InvariantCultureIgnoreCase));


var food = ConsoleHelper.GetInput("Type out a food you like: (tacos, pizza, burgers)", false, "tacos", "pizza", "burgers");

Console.WriteLine($"You like {food}");
var characterClass = ConsoleHelper.GetInputGeneric(
    $"What is your class? (Available Options: \n{CharacterClass.Warrior}\n{CharacterClass.Mage}\n{CharacterClass.Ranger}", CharacterClass.Warrior, CharacterClass.Mage, CharacterClass.Ranger);
Console.WriteLine($"Character Class: {characterClass}");

var stringVal = ConsoleHelper.GetInputGeneric("enter a string lol or tacos", "taco", "lol");
Console.WriteLine($"Entered string: {stringVal}");

//Console.WriteLine($"Your name is {name} and you are {age} years old and you like {pref} you have ${cash} in cash and your class is {characterClass} you said you are alive:  {aliva} you choose selection: {selection}");


