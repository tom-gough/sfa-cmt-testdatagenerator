using ScenarioBuilder.Builders;
using ScenarioBuilder.Models;

namespace ScenarioBuilder.Generator.Automation
{
    public class Generator
    {
        public static void Cohort_Provider_Draft(int size, bool nonLevyEmployer)
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithDefaultEmployer()
                .WithEditStatus(EditStatus.Provider)
                .WithLastAction(LastAction.None)
                .WithCommitmentStatus(CommitmentStatus.New)
                .WithApprenticeships(size);

            if (nonLevyEmployer)
            {
                builder.WithNonLevyEmployer();
            }

            builder.Build();
        }

        public static void Cohort_Employer_Draft(int size, bool nonLevyEmployer)
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithDefaultEmployer()
                .WithEditStatus(EditStatus.Employer)
                .WithLastAction(LastAction.None)
                .WithCommitmentStatus(CommitmentStatus.New)
                .WithApprenticeships(size);

            if (nonLevyEmployer)
            {
                builder.WithNonLevyEmployer();
            }

            builder.Build();
        }
    }
}
