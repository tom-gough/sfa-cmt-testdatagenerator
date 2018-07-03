using System;
using CommitmentsDataGen.Builders;
using CommitmentsDataGen.Models;

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
                .WithDefaultEmployerProvider(RelationshipOption.Undefined)
                .WithEditStatus(EditStatus.Provider)
                .WithLastAction(LastAction.Amend)
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder));
            builder.Build();
        }



        public static void Scenario_Transfer_Cohort_Employer_Draft()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Employer)
                .WithLastAction(LastAction.None)
                .WithCommitmentStatus(CommitmentStatus.New)
                .WithTransferSender(30060, "Silly Bears", null)
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder));
            builder.Build();
        }


        public static void Scenario_Transfer_Cohort_Rejected_By_Sender()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Employer)
                .WithLastAction(LastAction.Approve)
                .WithTransferSender(30060, "Silly Bears", TransferApprovalStatus.Rejected)
                .WithApprenticeships(10);
            builder.Build();

        }
        public static void Scenario_Transfer_Cohort_Pending_With_Sender()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithTransferSender(30060, "Silly Bears", TransferApprovalStatus.Pending)
                .WithApprenticeships(10);
            builder.Build();

        }

        public static void Scenario_Transfer_Cohort_Pending_With_Sender_With_FundingBands()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithTransferSender(30060, "Silly Bears", TransferApprovalStatus.Pending)
                .WithApprenticeship(cohort =>new ApprenticeshipBuilder(builder)
                    .WithTrainingCourse(new TrainingCourse
                    {
                        Id = "2",
                        Title = "Software developer"
                    })
                    .WithCost(20000))
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder)
                    .WithTrainingCourse(new TrainingCourse
                    {
                        Id = "1",
                        Title = "Network engineer"
                    })
                    .WithCost(16000))
                ;
            builder.Build();
        }

        public static void Scenario_Fully_Approved_Cohort()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeships(50);
            builder.Build();

        }


        public static void Scenario_Fully_Approved_Transfer_Cohort()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithTransferSender(30060, "Silly Bears", TransferApprovalStatus.Approved)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeships(50);
            builder.Build();

        }

        public static void Scenario_Fully_Approved_Cohort_With_Provider_Removed_From_ROATP()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployer()
                .WithProvider(99999999, "Bad Provider", RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeships(50);
            builder.Build();

        }

        public static void Scenario_Fully_Approved_Apprentices_With_DataLock_Success()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed) //todo: move these status fields into WithApprenticeships() method?
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
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed) //todo: move these status fields into WithApprenticeships() method?
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
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithTransferSender(30060, "Silly Bears", TransferApprovalStatus.Approved)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed) //todo: move these status fields into WithApprenticeships() method?
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
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithTransferSender(30060, "Silly Bears", TransferApprovalStatus.Approved)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed) //todo: move these status fields into WithApprenticeships() method?
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
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Both)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithApprenticeship(cohort =>
                    new ApprenticeshipBuilder(builder)
                        .WithDataLockSuccess()
                        .WithStartOption(ApprenticeshipStartedOption.Started)
                        .WithDataLock(DataLockType.Price)
                    );
            builder.Build();

        }

        public static void CourseDataLock()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
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

        public static void DataLockDueToPriceChangeMidway()
        {

            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
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

        public static void ReusingUln()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
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
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
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

        public static void ReusingUlnWhenPendingWithSender()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithTransferSender(30060, "Silly Bears", TransferApprovalStatus.Pending)
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
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
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

        public static void ManyApproved()
        {
            for (var i = 0; i < 10; i++)
            {
                var builder = new CohortBuilder();

                builder
                    .WithDefaultEmployerProvider(RelationshipOption.Defined)
                    .WithEditStatus(EditStatus.Both)
                    .WithLastAction(LastAction.Approve)
                    .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed) //todo: move these status fields into WithApprenticeships() method?
                    .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                    .WithApprenticeships(20);
                builder.Build();
            }
        }

        public static void Scenario_Very_Large_Cohort()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Provider)
                .WithApprenticeshipAgreementStatus(AgreementStatus.EmployerAgreed)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(50);
            builder.Build();
        }

        public static void Scenario_Multiple_Approved_Apprenticeships_Employers_And_Providers()
        {
            var builder = new CohortBuilder();

            builder
                .WithEmployer(8194, "1234", "ASAP CATERING LIMITED (Stub)")
                .WithProvider(10005124, "Plumpton College", RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Both)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder.Build();

            builder = new CohortBuilder();

            builder
                .WithEmployer(8194, "1234", "ASAP CATERING LIMITED (Stub)")
                .WithProvider(10005077, "Peterborough College", RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Both)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder.Build();



            //Silly Bears = 30060
            builder = new CohortBuilder();
            builder
                .WithEmployer(30060, "3884", "BUTCHER AND CHEESE MAKER LTD (Stub)")
                .WithProvider(10005124, "Plumpton College", RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Both)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder.Build();

            builder = new CohortBuilder();
            builder
                .WithEmployer(30060, "3884", "BUTCHER AND CHEESE MAKER LTD (Stub)")
                .WithProvider(10005077, "Peterborough College", RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Both)
                .WithApprenticeshipAgreementStatus(AgreementStatus.BothAgreed)
                .WithApprenticeshipPaymentStatus(PaymentStatus.Active)
                .WithLastAction(LastAction.Approve)
                .WithApprenticeships(1);
            builder.Build();

        }

    }
}
