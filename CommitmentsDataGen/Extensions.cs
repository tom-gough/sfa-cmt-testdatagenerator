using System;
using System.Collections.Generic;
using CommitmentsDataGen.Helpers;

namespace CommitmentsDataGen
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
