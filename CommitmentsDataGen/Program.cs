using System;
using CommitmentsDataGen.Generator;
using ScenarioBuilder.Helpers;

namespace CommitmentsDataGen
{
    class Program
    {
        static void Main(string[] args)
        {
            var scenarios = Scenarios.GetScenarios();

            Console.WriteLine("Select a scenario:");
            Console.WriteLine();

            var count = 0;
            foreach (var scenario in scenarios)
            {
                Console.WriteLine($"{count}: {scenario.Title}");
                count++;
            }

            Console.WriteLine();
            Console.Write("Scenario: ");

            var input = Convert.ToInt32(Console.ReadLine());

            var selected = scenarios[input];

            DbHelper.ClearDb();
            selected.Action.Invoke();

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
