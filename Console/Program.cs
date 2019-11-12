using System;
using ScenarioBuilder.Generator;
using ScenarioBuilder.Helpers;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var scenarios = Scenarios.GetScenarios();
            

            System.Console.WriteLine("Select a scenario:");
            System.Console.WriteLine();

            var count = 0;
            foreach (var scenario in scenarios)
            {
                System.Console.WriteLine($"{count}: {scenario.Title}");
                count++;
            }

            System.Console.WriteLine();
            System.Console.Write("Scenario: ");

            var input = Convert.ToInt32(System.Console.ReadLine());

            var selected = scenarios[input];

            DbHelper.ClearDb();
            selected.Action.Invoke();

            System.Console.WriteLine("Done!");
            System.Console.ReadKey();
        }
    }
}
