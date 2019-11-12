using System;
using CommitmentsDataGen.Builders;
using ScenarioBuilder.Helpers;
using ScenarioBuilder.Models;

namespace CommitmentsDataGen.Generator
{
    public static class Generator
    {
        public static void Scenario_Empty_Db()
        {
        }

        public static void Scenario_Cohort_Sent_to_new_Provider()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Provider)
                .WithLastAction(LastAction.Amend)
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder));
            builder.Build();
        }

        public static void Scenario_Cohort_Sent_to_existing_Provider()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Provider)
                .WithLastAction(LastAction.Approve)
                .WithLastUpdatedByEmployer("Chris", "chrisfoster186@googlemail.com")
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder));
            builder.Build();
        }

        public static void Scenario_Cohort_Approved_And_Sent_to_existing_Provider()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.EmployerAgreed)
                .WithLastUpdatedByEmployer("Chris", "chrisfoster186@googlemail.com")
                .WithApprenticeships(1000);
            builder.Build();
        }

        public static void Scenario_Transfer_Cohort_Employer_Draft()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithEditStatus(EditStatus.Employer)
                .WithLastAction(LastAction.None)
                .WithCommitmentStatus(CommitmentStatus.New)
                .WithTransferSender(8194, "Mega Corp", null)
                .WithApprenticeships(10);
            builder.Build();
        }
 
        public static void Scenario_Cohort_Employer_Draft()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithDefaultEmployer()
                .WithEditStatus(EditStatus.Employer)
                .WithLastAction(LastAction.None)
                .WithCommitmentStatus(CommitmentStatus.New)
                .WithApprenticeships(10);
            builder.Build();
        }

        public static void Scenario_Transfer_Cohort_Provider()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithEditStatus(EditStatus.Provider)
                .WithLastAction(LastAction.Approve)
                .WithCommitmentStatus(CommitmentStatus.Active)
                .WithApprenticeshipAgreementStatus(AgreementStatus
                    .EmployerAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithTransferSender(8194, "Mega Corp", null)
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder));
            builder.Build();
        }

        public static void Scenario_Transfer_Cohort_NonLevy_Employer()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithEditStatus(EditStatus.Employer)
                .WithLastAction(LastAction.Approve)
                .WithCommitmentStatus(CommitmentStatus.Active)
                .WithApprenticeshipAgreementStatus(AgreementStatus
                    .ProviderAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithTransferSender(8194, "Mega Corp", null)
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder));
            builder.Build();
        }

        public static void Scenario_Cohort_With_NonLevy_Employer_Draft()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithEditStatus(EditStatus.Employer)
                .WithLastAction(LastAction.None)
                .WithCommitmentStatus(CommitmentStatus.New)
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder));
            builder.Build();
        }



        public static void Scenario_Transfer_Cohort_Rejected_By_Sender()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithEditStatus(EditStatus.Employer)
                .WithLastAction(LastAction.Approve)
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Rejected)
                .WithApprenticeships(10);
            builder.Build();

        }

        public static void Scenario_Transfer_Cohort_Pending_With_Sender()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipPaymentStatus(PaymentStatus.PendingApproval, DateTime.UtcNow)
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Pending)
                .WithLastUpdatedByEmployer("Chris Foster Employer User", "chrisfoster186@googlemail.com")
                .WithLastUpdatedByProvider("Chris Foster Provider User", "chrisfoster186@googlemail.com")
                .WithApprenticeships(10);
            builder.Build();

        }

        public static void Scenario_Transfer_Cohort_Pending_With_Sender_With_FundingBands()
        {
            var builder = new CohortBuilder();

            builder
                .WithNonLevyEmployer()
                .WithDefaultProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Pending)
                .WithApprenticeships(10)
                .WithFundingCapWarning();
            builder.Build();
        }

        public static void Scenario_Fully_Approved_Cohort()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus
                    .BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeships(1);
            builder.Build();

        }

        public static void Scenario_Fully_Approved_Cohort_NonLevy_WithReservation()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultProvider()
                .WithNonLevyEmployer()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus
                    .BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithReservations()
                .WithApprenticeships(1);
            builder.Build();

        }

        public static void Scenario_Fully_Approved_Transfer_Cohort()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Approved)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus
                    .BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeships(50);
            builder.Build();

        }

        public static void Scenario_Fully_Approved_Cohort_With_Provider_Removed_From_ROATP()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployer()
                .WithProvider(99999999, "Bad Provider")
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus
                    .BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeships(50);
            builder.Build();

        }

        public static void Scenario_Fully_Approved_Apprentices_With_DataLock_Success()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus
                    .BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.Started))
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.WaitingToStart))
                .Build();

        }


        public static void Scenario_Fully_Approved_Apprentices_Pending_DataLock_Success()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus
                    .BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.Started))
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.WaitingToStart))
                .Build();

        }




        public static void Scenario_Fully_Approved_Transfer_Apprentices_With_DataLock_Success()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Approved)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus
                    .BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.Started))
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.WaitingToStart))
                .Build();

        }


        public static void Scenario_Fully_Approved_Transfer_Apprentices_Pending_DataLock_Success()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Approved)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus
                    .BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.Started))
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.WaitingToStart))
                .Build();

        }

        public static void PriceDataLock()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        //.WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithDataLock(DataLockType.Price)
                );
            builder.Build();

        }

        public static void MulitplePriceDataLocks()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithDataLock(DataLockType.MultiPrice)
                );
            builder.Build();

        }


        public static void CourseDataLock()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithDataLock(DataLockType.Course)
                );
            builder.Build();

        }

        public static void CourseDataLockWithoutDataLockSuccess()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithDataLock(DataLockType.Course)
                );
            builder.Build();

        }

        public static void DataLockDueToPriceChangeMidway()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithDataLock(DataLockType.PriceChangeMidway)
                );
            builder.Build();

        }

        public static void PriceDataLockAndPendingChangeOfCircumstances()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        //.WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithDataLock(DataLockType.Price)
                        .WithChangeOfCircumstances()
                );
            builder.Build();



        }

        public static void ReusingUln()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 6, 1))
                        .WithEndOption(new DateTime(2019, 6, 23))
                        .WithStopOption(new DateTime(2018, 9, 12))
                );
            builder.Build();


            builder = new CohortBuilder();
            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Provider)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.EmployerAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.PendingApproval)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 9, 1))
                );
            builder.Build();
        }

        public static void TwoApprovedApprenticeshipsWithSameUln()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 6, 1))
                        .WithEndOption(new DateTime(2019, 6, 23))
                        .WithStopOption(new DateTime(2018, 9, 12))
                );
            builder.Build();


            builder = new CohortBuilder();
            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 9, 1))
                );
            builder.Build();
        }



        public static void ReusingUlnWhenPendingWithSender()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithTransferSender(8194, "Mega Corp", TransferApprovalStatus.Pending)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.PendingApproval)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 6, 1))
                );
            builder.Build();


            builder = new CohortBuilder();
            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Employer)
                .WithLastAction(LastAction.None)
                .WithApprenticeshipAgreementStatus(AgreementStatus.NotAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.PendingApproval)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithUln("1000001880")
                        .WithStartOption(new DateTime(2018, 9, 1))
                );
            builder.Build();
        }

        public static void SingleApproved()
        {
            var builder = new CohortBuilder();
            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus
                    .BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder).WithStartOption(new DateTime(2018, 9, 1)));

            builder.Build();
        }

        public static void ManyApproved()
        {
            for (var i = 0; i < 100; i++)
            {
                var builder = new CohortBuilder();

                if (RandomHelper.GetRandomNumber(10) > 8)
                {
                    builder
                        .WithDefaultEmployerProvider()
                        .WithEditStatus(EditStatus.Both)
                        .WithLastAction(LastAction.Approve)
                        .WithApprenticeshipAgreementStatus(AgreementStatus
                            .BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                        .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                        .WithApprenticeships(1);
                }
                else
                {
                    DataLockType datalockType = DataLockType.Course;
                    var r = RandomHelper.GetRandomNumber(3);
                    if (r == 1)
                    {
                        datalockType = DataLockType.Price;
                    }

                    if (r == 2)
                    {
                        datalockType = DataLockType.MultiPrice;
                    }

                    builder
                        .WithDefaultEmployerProvider()
                        .WithEditStatus(EditStatus.Both)
                        .WithLastAction(LastAction.Approve)
                        .WithApprenticeshipAgreementStatus(AgreementStatus
                            .BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                        .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                        .WithApprenticeship(cohort =>
                            new ApprenticeshipBuilder(builder)
                                .WithDataLock(datalockType));
                }

                builder.Build();
            }
        }

        public static void Scenario_Very_Large_Cohort()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Provider)
                .WithApprenticeshipAgreementStatus(AgreementStatus.EmployerAgreed)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(500);
            builder.Build();
        }

        public static void Scenario_Very_Large_Cohort_Ready_For_Employer()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Employer)
                .WithApprenticeshipAgreementStatus(AgreementStatus.ProviderAgreed)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(500);
            builder.Build();
        }

        public static void ManyApproved_And_Very_Large_Cohort_Ready_For_Employer()
        {
            for (var i = 0; i < 15; i++)
            {
                var builder = new CohortBuilder();

                builder
                    .WithDefaultEmployerProvider()
                    .WithEditStatus(EditStatus.Both)
                    .WithLastAction(LastAction.Approve)
                    .WithApprenticeshipAgreementStatus(AgreementStatus
                        .BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                    .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                    .WithApprenticeships(1000);
                builder.Build();

            }

            var builder2 = new CohortBuilder();

            builder2
                .WithDefaultEmployerProvider()
                .WithEditStatus(EditStatus.Employer)
                .WithApprenticeshipAgreementStatus(AgreementStatus.ProviderAgreed)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder2.Build();
        }


        public static void Scenario_Multiple_Approved_Apprenticeships_Employers_And_Providers()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployer()
                .WithDefaultProvider()
                .WithEditStatus(EditStatus.Both)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder.Build();

            builder = new CohortBuilder();

            builder
                .WithDefaultEmployer()
                .WithProvider(10005077, "Train-U-Good Corporation")
                .WithEditStatus(EditStatus.Both)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder.Build();

            builder = new CohortBuilder();
            builder
                .WithEmployer(30060, "06344082", "Rapid Logistics Co Ltd", "7EKPG7")
                .WithDefaultProvider()
                .WithEditStatus(EditStatus.Both)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder.Build();

            builder = new CohortBuilder();
            builder
                .WithEmployer(30060, "06344082", "Rapid Logistics Co Ltd", "7EKPG7")
                .WithProvider(10005077, "Train-U-Good Corporation")
                .WithEditStatus(EditStatus.Both)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder.Build();

        }


        public static void EmployerWithMultipleProviders()
        {
            CreateAFewApprovedApprenticesForProvider(10005077, "Train-U-Good Corporation");
            CreateAFewApprovedApprenticesForProvider(10005078, "Amazing Training Ltd");
            CreateAFewApprovedApprenticesForProvider(10005079, "Like a Pro Education Inc.");
        }

        public static void EmployerWithMultipleProvidersV2()
        {
            CreateAFewApprovedApprenticesForProvider(10005078, "Amazing Training Ltd");
            CreateAFewApprovedApprenticesForProvider(10005079, "Like a Pro Education Inc.");
        }

        private static void CreateAFewApprovedApprenticesForProvider(int providerId, string providerName)
        {
            var approvalDateTime = DateTime.UtcNow.Date.AddDays(-100);

            for (var i = 0; i < 3; i++)
            {
                var builder = new CohortBuilder();
                builder
                    .WithDefaultEmployer()
                    .WithProvider(providerId, providerName)
                    .WithEditStatus(EditStatus.Both)
                    .WithLastAction(LastAction.Approve)
                    .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                    .WithApprenticeshipPaymentStatus(PaymentStatus.Active, DataHelper.GetRandomDateTime())
                    .WithApprenticeships(3);
                builder.Build();
            }
        }
    }
}
