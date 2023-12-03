using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string filepath = @"..\..\..\Input.txt";
        string[] gameRecords = File.ReadAllLines(filepath);
        int totalIdSum = 0;
        long totalPowerSum = 0; // Variable for the sum of powers of all games

        // Iterate through each game record
        foreach (var record in gameRecords)
        {
            var minCubes = FindMinimumCubes(record);
            totalPowerSum += minCubes;
            // Check if the game is possible with the given bag configuration
            if (IsGamePossible(record))
            {
                // Add the game ID to the total sum if the game is possible
                totalIdSum += GetGameId(record);
            }
        }

        // Output the total sum of the IDs of possible games
        Console.WriteLine("Sum of IDs of possible games: " + totalIdSum);
        Console.WriteLine($"Sum of the power of minimum sets {totalPowerSum}");

    }

    static int FindMinimumCubes(string record)
    {
        int maxRed = 0, maxGreen = 0, maxBlue = 0;

        var subsets = record.Split(';');
        foreach (var subset in subsets)
        {
            int red = GetColorCount(subset, "red");
            int green = GetColorCount(subset, "green");
            int blue = GetColorCount(subset, "blue");

            maxRed = Math.Max(maxRed, red);
            maxGreen = Math.Max(maxGreen, green);
            maxBlue = Math.Max(maxBlue, blue);
        }

        return maxRed * maxGreen * maxBlue;
    }




    static bool IsGamePossible(string record)
    {
        int maxRed = 12, maxGreen = 13, maxBlue = 14;

        var subsets = record.Split(';');
        foreach (var subset in subsets)
        {
            // Get the count of each color in the subset
            int red = GetColorCount(subset, "red");
            int green = GetColorCount(subset, "green");
            int blue = GetColorCount(subset, "blue");

            // Check if any color count exceeds the bag's capacity
            if (red > maxRed || green > maxGreen || blue > maxBlue)
                return false; // Game is not possible if any color exceeds the limit
        }
        return true; // Game is possible if all subsets are within the limit
    }

    static int GetColorCount(string subset, string color)
    {
        var match = Regex.Match(subset, $@"(\d+) {color}");

        return match.Success ? int.Parse(match.Groups[1].Value) : 0;
    }

    // Method to extract the game ID from a game record
    static int GetGameId(string record)
    {
        // Use regular expression to find the game ID
        var match = Regex.Match(record, @"Game (\d+):");
        // Return the ID if found, otherwise return 0
        return match.Success ? int.Parse(match.Groups[1].Value) : 0;
    }
}