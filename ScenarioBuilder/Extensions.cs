using System;
using System.Collections.Generic;
using ScenarioBuilder.Helpers;

namespace ScenarioBuilder
{
    public static class Extensions
    {
        public static T RandomElement<T>(this IList<T> q)
        {
            var r = new Random();
            return q[RandomHelper.GetRandomNumber(q.Count)];
        }
    }
}
