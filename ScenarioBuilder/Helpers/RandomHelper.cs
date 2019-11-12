using System;

namespace ScenarioBuilder.Helpers
{
    public static class RandomHelper
    {
        private static readonly Random Rnd = new Random();

        public static int GetRandomNumber(int limit)
        {
            return Rnd.Next(limit);
        }
    }
}
