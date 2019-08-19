using System;
using System.Collections.Generic;
using CommitmentsDataGen.Helpers;
using CommitmentsDataGen.Models;

namespace CommitmentsDataGen.Builders
{
    public class ApprenticeshipBuilder
    {
        public long Id { get; private set; }
        public ApprenticeshipStartedOption Started { get; private set; }
        public DateTime? ExplicitStartDate { get; private set; }
        public DateTime? ExplicitEndDate { get; private set; }
        public DateTime? ExplicitStopDate { get; private set; }
        public ApprenticeshipStopOption Stopped { get; private set; }
        public bool HasHadDataLockSuccess { get; private set; }
        public bool HasChangeOfCircumstances { get; set; }
        public TrainingCourse TrainingCourse { get; private set; }
        public int Cost { get; private set; }
        public string Uln { get; private set; }
        public CohortBuilder CohortBuilder { get; private set; }
        
        public DataLockType DataLock { get; private set; }

        public ApprenticeshipBuilder(CohortBuilder cohortBuilder)
        {
            CohortBuilder = cohortBuilder;
            Id = IdentityHelpers.GetNextApprenticeshipId();
            Started = ApprenticeshipStartedOption.Random;
            Stopped = ApprenticeshipStopOption.NotStopped;
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

        public ApprenticeshipBuilder WithStartOption(DateTime startDate)
        {
            ExplicitStartDate = startDate;
            return this;
        }

        public ApprenticeshipBuilder WithEndOption(DateTime endDate)
        {
            ExplicitEndDate = endDate;
            return this;
        }

        public ApprenticeshipBuilder WithStopOption(ApprenticeshipStopOption stopOption)
        {
            Stopped = stopOption;
            return this;
        }
        public ApprenticeshipBuilder WithStopOption(DateTime stopDate)
        {
            ExplicitStopDate = stopDate;
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

        public ApprenticeshipBuilder WithUln(string uln)
        {
            Uln = uln;
            return this;
        }

        public ApprenticeshipBuilder WithDataLock(DataLockType dataLockType)
        {
            DataLock = dataLockType;
            return this;
        }

        public ApprenticeshipBuilder WithChangeOfCircumstances()
        {
            HasChangeOfCircumstances = true;
            return this;
        }


        public Apprenticeship Build()
        {
            DateTime? startDate = GenerateStartDate();
            var endDate = GenerateEndDate(startDate);
            var stopDate = GenerateStopDate(startDate, endDate);
           

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
                EndDate = endDate,
                ULN = String.IsNullOrEmpty(Uln) ? DataHelper.GetRandomUniqueULN() : Uln,
                TrainingCode = TrainingCourse.Id,
                TrainingType = TrainingCourse.TrainingType,
                TrainingName = TrainingCourse.TitleRestrictedLength,
                Cost = Cost,
                AgreementStatus = CohortBuilder.AgreementStatus,
                PaymentStatus = stopDate.HasValue ? PaymentStatus.Cancelled : CohortBuilder.PaymentStatus,
                PaymentOrder = CohortBuilder.PaymentStatus == PaymentStatus.PendingApproval ? default(int?) : 0,
                HasHadDataLockSuccess = HasHadDataLockSuccess,
                HasChangeOfCircumstances = HasChangeOfCircumstances,
                CreatedOn = DateTime.UtcNow,
                StopDate = stopDate,
                AgreedOn = CohortBuilder.AgreedOnDate,
                ReservationId = CohortBuilder.HasReservations ? Guid.NewGuid() : default(Guid?)
            };

            //data locks
            if (DataLock == DataLockType.Price)
            {
                apprenticeship.DataLocks.Add(new DataLock
                {
                    ErrorCode = 64,
                    IlrEffectiveFromDate = apprenticeship.StartDate.Value,
                    IlrTotalCost = apprenticeship.Cost * 2,
                    IlrTrainingCourseCode = apprenticeship.TrainingCode,
                    IlrTrainingType = apprenticeship.TrainingType,
                    PriceEpisodeIdentifier = apprenticeship.TrainingCode + '-' + apprenticeship.StartDate.Value.ToShortDateString(),
                    Status = 2
                });
            }
            if (DataLock == DataLockType.PriceChangeMidway)
            {
                apprenticeship.DataLocks.Add(new DataLock
                { //successful dlock
                    ErrorCode = 64,
                    IlrEffectiveFromDate = apprenticeship.StartDate.Value,
                    IlrTotalCost = apprenticeship.Cost,
                    IlrTrainingCourseCode = apprenticeship.TrainingCode,
                    IlrTrainingType = apprenticeship.TrainingType,
                    PriceEpisodeIdentifier = apprenticeship.TrainingCode + '-' + apprenticeship.StartDate.Value.ToShortDateString(),
                    Status = 1
                });
                var priceChangeDate = DataHelper
                    .GetMidwayPoint(apprenticeship.StartDate.Value, apprenticeship.EndDate.Value).Value;
                apprenticeship.DataLocks.Add(new DataLock
                { //price change dlock error
                    ErrorCode = 64,
                    IlrEffectiveFromDate = priceChangeDate,
                    IlrTotalCost = apprenticeship.Cost * 2,
                    IlrTrainingCourseCode = apprenticeship.TrainingCode,
                    IlrTrainingType = apprenticeship.TrainingType,
                    PriceEpisodeIdentifier = apprenticeship.TrainingCode + '-' + priceChangeDate.ToShortDateString(),
                    Status = 2
                });
            }

            if (DataLock == DataLockType.Course)
            {
                var dlockCourse = DataHelper.GetRandomTrainingCourse();
                apprenticeship.DataLocks.Add(new DataLock
                {
                    ErrorCode = 4,
                    IlrEffectiveFromDate = apprenticeship.StartDate.Value.AddDays(15),
                    IlrTotalCost = apprenticeship.Cost,
                    IlrTrainingCourseCode = dlockCourse.Id,
                    IlrTrainingType = dlockCourse.TrainingType,
                    PriceEpisodeIdentifier = dlockCourse.Id + '-' + apprenticeship.StartDate.Value.ToShortDateString(),
                    Status = 2
                });
            }

            if (DataLock == DataLockType.MultiPrice)
            {
                var priceChangeDate = DataHelper
                    .GetMidwayPoint(apprenticeship.StartDate.Value, apprenticeship.EndDate.Value).Value;
                apprenticeship.DataLocks.Add(new DataLock
                {
                    ErrorCode = 64,
                    IlrEffectiveFromDate = apprenticeship.StartDate.Value,
                    IlrTotalCost = apprenticeship.Cost * 2,
                    IlrTrainingCourseCode = apprenticeship.TrainingCode,
                    IlrTrainingType = apprenticeship.TrainingType,
                    PriceEpisodeIdentifier = apprenticeship.TrainingCode + '-' + apprenticeship.StartDate.Value.ToShortDateString(),
                    Status = 2
                });
                apprenticeship.DataLocks.Add(new DataLock
                {
                    ErrorCode = 64,
                    IlrEffectiveFromDate = priceChangeDate,
                    IlrTotalCost = apprenticeship.Cost * 3,
                    IlrTrainingCourseCode = apprenticeship.TrainingCode,
                    IlrTrainingType = apprenticeship.TrainingType,
                    PriceEpisodeIdentifier = apprenticeship.TrainingCode + '-' + priceChangeDate.ToShortDateString(),
                    Status = 2
                });
            }

            return apprenticeship;
        }

        private DateTime? GenerateStartDate()
        {
            if (ExplicitStartDate.HasValue)
            {
                return ExplicitStartDate;
            }

            switch (Started)
            {
                case ApprenticeshipStartedOption.Started:
                    return DataHelper.GetRandomStartDateInPast();
                    break;
                case ApprenticeshipStartedOption.Random:
                    return DataHelper.GetRandomStartDate();
                    break;
                case ApprenticeshipStartedOption.WaitingToStart:
                    return DataHelper.GetRandomStartDateInFuture();
                    break;
                default:
                    return null;
                    break;
            }
        }

        private DateTime? GenerateEndDate(DateTime? startDate)
        {
            if (ExplicitEndDate.HasValue)
            {
                return ExplicitEndDate;
            }

            return DataHelper.GetRandomEndDate(startDate);
        }

        private DateTime? GenerateStopDate(DateTime? startDate, DateTime? endDate)
        {
            if (ExplicitStopDate.HasValue)
            {
                return ExplicitStopDate;
            }

            if (Stopped == ApprenticeshipStopOption.NotStopped)
            {
                return null;
            }

            if (Stopped == ApprenticeshipStopOption.Midway)
            {
                if (!startDate.HasValue || !endDate.HasValue)
                {
                    return null;
                }

                return DataHelper.GetMidwayPoint(startDate.Value, endDate.Value);
            }

            if (Stopped == ApprenticeshipStopOption.StoppedAndBackdatedToStart)
            {
                return startDate;
            }

            return null;
        }


    }

    public enum ApprenticeshipStartedOption
    {
        Unspecified = 0,
        Random = 1,
        Started = 2,
        WaitingToStart = 3
    }

    public enum ApprenticeshipStopOption
    {
        NotStopped = 0,
        StoppedAndBackdatedToStart = 1,
        Midway = 2
    }

}
