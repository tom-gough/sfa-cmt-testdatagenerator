using System;
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
                .WithParty(Party.Provider)
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
                .WithParty(Party.Employer)
                .WithLastAction(LastAction.None)
                .WithCommitmentStatus(CommitmentStatus.New)
                .WithApprenticeships(size);

            if (nonLevyEmployer)
            {
                builder.WithNonLevyEmployer();
            }

            builder.Build();
        }


        public static void Single_Stopped_Apprentice()
        {
            var builder = new CohortBuilder();

            builder
                .WithNonLevyEmployer()
                .WithDefaultProvider()
                .WithParty(Party.None)
                .WithApprovals(Party.Employer | Party.Provider)
                .WithLastAction(LastAction.Approve)
                //.WithApprenticeshipPaymentStatus(PaymentStatus.Active) //with stop option will override this
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2019, 6, 1))
                        .WithEndOption(new DateTime(2020, 6, 1))
                        .WithStopOption(new DateTime(2020, 2, 1))
                );
            builder.Build();
        }
    }
}
