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


        public static void ITASK0109557()
        {
            var builder = new CohortBuilder();

            builder
                .WithDefaultEmployerProvider(RelationshipOption.Defined)
                .WithEditStatus(EditStatus.Employer)
                .WithLastAction(LastAction.Amend)
                .WithLastUpdatedByProvider("Provider user", "Provider email")
                .WithLastUpdatedByEmployer("Employer user", "Employer email")
                .WithApprenticeship(cohort => new ApprenticeshipBuilder(builder));
            builder.Build();
        }
    }
}
