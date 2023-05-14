public static class ConsoleHelper
{
    public static string GetInput(string prompt, bool ignoreCase = true, params string[] validResponses)
    {
        bool isValidResponse = false;
        string? response = string.Empty;
        do
        {
            Console.WriteLine(prompt);
            response = Console.ReadLine();
            if (validResponses.Length > 0)
            {
                for (int i = 0; i < validResponses.Length; i++)
                {
                    if (ignoreCase)
                    {
                        if (response.Equals(validResponses[i], StringComparison.InvariantCultureIgnoreCase))
                        {
                            isValidResponse = true;
                        }    
                    }
                    else
                    {
                        if (response.Equals(validResponses[i]))
                        {
                            isValidResponse = true;
                        }
                    }
                    
                }    
            }
            else
            {
                isValidResponse = true;
            }

            if (string.IsNullOrWhiteSpace(response))
            {
                isValidResponse = false;
            }
            
            if (isValidResponse == false)
            {
                Console.WriteLine("Invalid Response, Try again");
            }
        } while (!isValidResponse);

        return response;
    }

    public static T? GetInputGeneric<T>(string prompt, params T[] validResponses)
    {
        bool isValidResponse = false;
        T result;
        string? response = string.Empty;
        Type resultType = typeof(T);
        try
        {
            do
            {
                Console.WriteLine(prompt);
                response = Console.ReadLine();
                if (validResponses.Length > 0)
                {
                    for (int i = 0; i < validResponses.Length; i++)
                    {
                        var r = validResponses[i];
                        bool equals = false;
                        if (resultType.IsEnum)
                        {
                            var enumResponse = Enum.Parse(resultType, response, true);
                            if (r != null) equals = r.Equals(enumResponse);
                        }
                        else
                        {
                            if (r != null) equals = r.Equals((T)Convert.ChangeType(response, resultType)!);
                        }
                        if (response != null && equals)
                        {
                            isValidResponse = true;
                            if (resultType == typeof(bool))
                            {
                                var boolResponse = Convert.ToBoolean(Enum.Parse(typeof(BoolAlias), response));
                                return (T)Convert.ChangeType(boolResponse, resultType);
                            }

                            if (resultType.IsEnum)
                            {
                                var enumResponse = Enum.Parse(resultType, response, true);
                                return (T)Convert.ChangeType(enumResponse, resultType);
                            }
                            return (T)Convert.ChangeType(response, resultType);
                        }
                    }
                }
                else
                {
                    if (response != null)
                    {
                        isValidResponse = true;
                        if (resultType == typeof(bool))
                        {
                            var boolResponse = Convert.ToBoolean(Enum.Parse(typeof(BoolAlias), response, true));
                            return (T)Convert.ChangeType(boolResponse, resultType);
                        }
                        if (resultType.IsEnum)
                        {
                            var enumResponse = Enum.Parse(resultType, response, true);
                            return (T)Convert.ChangeType(enumResponse, resultType);
                        }
                        return (T)Convert.ChangeType(response, resultType);
                    }
                }
                if (isValidResponse == false)
                {
                    Console.WriteLine("Invalid Response, Try again");
                }
            } while (!isValidResponse);
            
        }
        catch (Exception e)
        {
            Console.WriteLine("Invalid Response, Try again");
            return GetInputGeneric<T>(prompt, validResponses);
        }
        return default(T);
    }

    public static int RollDice(int sides, int amount = 1)
    {
        var rnd = new Random();
        int result = 0;
        for (int i = 0; i < amount; i++)
        {
            var roll = rnd.Next(1, sides + 1);
            Console.WriteLine($"Rolled {sides} sided dice {i + 1}: {roll}");
            result += roll;
        }

        Console.WriteLine($"Rolled {amount}D{sides} for a total of {result}");
        return result;
    }
    
    public static (int total, List<int> rolls) RollDiceGetRolls(int sides, int amount = 1)
    {
        var rnd = new Random();
        int result = 0;
        var rolls = new List<int>();
        for (int i = 0; i < amount; i++)
        {
            var roll = rnd.Next(1, sides + 1);
            Console.WriteLine($"Rolled {sides} sided dice {i + 1}: {roll}");
            rolls.Add(roll);
            result += roll;
        }

        Console.WriteLine($"Rolled {amount}D{sides} for a total of {result}");
        return (result, rolls);
    }
}