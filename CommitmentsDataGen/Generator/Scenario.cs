using System;

namespace CommitmentsDataGen.Generator
{
    public class Scenario
    {
        public string Title { get; }
        public Action Action { get; }

        public Scenario(string title, Action action)
        {
            Title = title;
            Action = action;
        }


    }
}
