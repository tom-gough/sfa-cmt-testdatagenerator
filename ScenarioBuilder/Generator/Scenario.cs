using System;

namespace ScenarioBuilder.Generator
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
