using System;
using CommitmentsDataGen.Helpers;
using CommitmentsDataGen.Models;

namespace CommitmentsDataGen.Builders
{
    public class ApprenticeshipBuilder
    {
        public long Id { get; private set; }
        public ApprenticeshipStartedOption Started { get; private set; }
        public bool HasHadDataLockSuccess { get; private set; }
        public TrainingCourse TrainingCourse { get; private set; }
        public int Cost { get; private set; }
        public CohortBuilder CohortBuilder { get; private set; }

        public ApprenticeshipBuilder(CohortBuilder cohortBuilder)
        {
            CohortBuilder = cohortBuilder;
            Id = IdentityHelpers.GetNextApprenticeshipId();
            Started = ApprenticeshipStartedOption.Random;
            Cost = 2000;
        }

        public ApprenticeshipBuilder WithDataLockSuccess()
        {
            HasHadDataLockSuccess = true;
            return this;
        }

        public ApprenticeshipBuilder WithStartOption(ApprenticeshipStartedOption option)
        {
            Started = option;
            return this;
        }

        public ApprenticeshipBuilder WithTrainingCourse(TrainingCourse course)
        {
            TrainingCourse = course;
            return this;
        }

        public ApprenticeshipBuilder WithCost(int cost)
        {
            Cost = cost;
            return this;
        }

        public Apprenticeship Build()
        {
            DateTime? startDate = null;

            switch (Started)
            {
                case ApprenticeshipStartedOption.Started:
                    startDate = DataHelper.GetRandomStartDateInPast();
                    break;
                case ApprenticeshipStartedOption.Random:
                    startDate = DataHelper.GetRandomStartDate();
                    break;
                case ApprenticeshipStartedOption.WaitingToStart:
                    startDate = DataHelper.GetRandomStartDateInFuture();
                    break;
                default:
                    startDate = null;
                    break;
            }
            
            //todo: perhaps select a training course valid on the start date?

            if (TrainingCourse == null)
            {
                TrainingCourse = CohortBuilder.IsPaidByTransfer
                    ? DataHelper.GetRandomTrainingCourse(TrainingProgrammeTypeFilter.Standard)
                    : DataHelper.GetRandomTrainingCourse(TrainingProgrammeTypeFilter.All);
            }

            var apprenticeship = new Apprenticeship
            {
                Id = Id,
                CommitmentId = CohortBuilder.Id,
                FirstName = DataHelper.GetRandomFirstName(),
                LastName = DataHelper.GetRandomLastName(),
                DateOfBirth = DataHelper.GetRandomDateOfBirth(),
                StartDate = startDate,
                EndDate = DataHelper.GetRandomEndDate(startDate),
                ULN = DataHelper.GetRandomUniqueULN(),
                TrainingCode = TrainingCourse.Id,
                TrainingType = TrainingCourse.TrainingType,
                TrainingName = TrainingCourse.TitleRestrictedLength,
                Cost = Cost,
                AgreementStatus = CohortBuilder.AgreementStatus,
                PaymentStatus = CohortBuilder.PaymentStatus,
                HasHadDataLockSuccess = HasHadDataLockSuccess,
                CreatedOn = DateTime.UtcNow
            };

            return apprenticeship;
        }
    }

    public enum ApprenticeshipStartedOption
    {
        Unspecified = 0,
        Random = 1,
        Started = 2,
        WaitingToStart = 3
    }

}
