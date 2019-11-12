using System;
using ScenarioBuilder.Generator;
using ScenarioBuilder.Helpers;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine("Commitments Test Data Generator");
            System.Console.WriteLine();
            System.Console.ForegroundColor = ConsoleColor.White;
            
            var scenarios = Scenarios.GetScenarios();

            while (true)
            {
                var count = 0;
                foreach (var scenario in scenarios)
                {
                    System.Console.WriteLine($"{count}: {scenario.Title}");
                    count++;
                }

                System.Console.WriteLine();
                System.Console.Write("Select scenario (enter quits): ");

                var stringInput = System.Console.ReadLine();
                if (string.IsNullOrWhiteSpace(stringInput))
                {
                    break;
                }

                var input = Convert.ToInt32(stringInput);

                var selected = scenarios[input];

                DbHelper.ClearDb();
                IdentityHelpers.ClearAll();
                selected.Action.Invoke();

                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("Scenario created");
                System.Console.WriteLine();
                System.Console.ForegroundColor = ConsoleColor.White;
                System.Console.WriteLine("Press a key...");
                System.Console.ReadKey();
                System.Console.WriteLine();
            }
          
        }
    }
}
