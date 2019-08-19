using System;
using System.Collections.Generic;
using CommitmentsDataGen.Helpers;
using CommitmentsDataGen.Models;

namespace CommitmentsDataGen.Builders
{
    public class CohortBuilder
    {
        public long Id => _commitment.Id;
        public bool IsPaidByTransfer => _commitment.TransferSenderId.HasValue;

        public AgreementStatus AgreementStatus { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public bool HasReservations { get; private set; } = true;
        public DateTime? AgreedOnDate { get; private set; }
        
        private readonly Commitment _commitment;
        private readonly List<ApprenticeshipBuilder> _apprenticeshipBuilders;

        public CohortBuilder()
        {
            var id = IdentityHelpers.GetNextCohortId();

            //todo: most of this should be pushed into WithXXX methods
            _commitment = new Commitment
            {
                Id = id,
                Reference = HashingHelper.Encoder(id),
                CommitmentStatus = CommitmentStatus.Active,
                EditStatus = EditStatus.Employer,
                CreatedOn = System.DateTime.Now,
                LastAction = LastAction.None,
                TransferSenderId = null,
                TransferSenderName = "",
                TransferApprovalStatus = null,
                LastUpdatedByEmployerName = "",
                LastUpdatedByEmployerEmail = "",
                LastUpdatedByProviderName = "",
                LastUpdatedByProviderEmail = ""
            };

            _apprenticeshipBuilders = new List<ApprenticeshipBuilder>();
        }

        public CohortBuilder WithDefaultEmployerProvider()
        {
            _commitment.EmployerAccountId = 8194;
            _commitment.LegalEntityId = "736281";
            _commitment.LegalEntityName = "MegaCorp Pharmaceuticals";
            _commitment.LegalEntityAddress = "1 High Street";
            _commitment.LegalEntityOrganisationType = 1;
            _commitment.AccountLegalEntityPublicHashedId = "XEGE5X";

            _commitment.ProviderId = 10005077;
            _commitment.ProviderName = "Train-U-Good Corporation";

            return this;
        }

        public CohortBuilder WithDefaultEmployer()
        {
            _commitment.EmployerAccountId = 8194;
            _commitment.LegalEntityId = "736281";
            _commitment.LegalEntityName = "MegaCorp Pharmaceuticals";
            _commitment.LegalEntityAddress = "1 High Street";
            _commitment.LegalEntityOrganisationType = 1;
            _commitment.AccountLegalEntityPublicHashedId = "XEGE5X";
            return this;
        }

        public CohortBuilder WithNonLevyEmployer()
        {
            _commitment.EmployerAccountId = 30060;
            _commitment.LegalEntityId = "736281";
            _commitment.LegalEntityName = "Rapid Logistics Co Ltd";
            _commitment.LegalEntityAddress = "1 High Street";
            _commitment.LegalEntityOrganisationType = 1;
            _commitment.AccountLegalEntityPublicHashedId = "X9JE72";
            return this;
        }


        public CohortBuilder WithDefaultProvider()
        {
            _commitment.ProviderId = 10005077;
            _commitment.ProviderName = "Train-U-Good Corporation";
            return this;
        }

        public CohortBuilder WithEmployer(long accountId, string legalEntityId, string name, string accountLegalEntityPublicHashedId)
        {
            _commitment.EmployerAccountId = accountId;
            _commitment.LegalEntityId = legalEntityId;
            _commitment.LegalEntityName = name;
            _commitment.LegalEntityAddress = "Some address";
            _commitment.LegalEntityOrganisationType = 1;
            _commitment.AccountLegalEntityPublicHashedId = accountLegalEntityPublicHashedId;
            return this;
        }

        public CohortBuilder WithProvider(int providerId, string providerName)
        {
            _commitment.ProviderId = providerId;
            _commitment.ProviderName = providerName;
            return this;
        }

        public CohortBuilder WithEditStatus(EditStatus status)
        {
            _commitment.EditStatus = status;
            return this;
        }

        public CohortBuilder WithTransferSender(long transferSenderId, string transferSenderName,
            TransferApprovalStatus? transferApprovalStatus)
        {
            _commitment.TransferSenderId = transferSenderId;
            _commitment.TransferSenderName = transferSenderName;
            _commitment.TransferApprovalStatus = transferApprovalStatus;

            return this;
        }

        public CohortBuilder WithApprenticeships(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _apprenticeshipBuilders.Add(new ApprenticeshipBuilder(this));
            }
            return this;
        }

        public CohortBuilder WithApprenticeship(Func<CohortBuilder,ApprenticeshipBuilder> apprenticeshipBuilder)
        {
            var builder = apprenticeshipBuilder.Invoke(this);
            _apprenticeshipBuilders.Add(builder);
            return this;
        }

        public CohortBuilder WithApprenticeshipAgreementStatus(AgreementStatus status)
        {
            AgreementStatus = status;
            return this;
        }

        public CohortBuilder WithReservations()
        {
            HasReservations = true;
            return this;
        }

        public CohortBuilder WithoutReservations()
        {
            HasReservations = false;
            return this;
        }

        public CohortBuilder WithApprenticeshipPaymentStatus(PaymentStatus status, DateTime? approvalDate = null)
        {
            PaymentStatus = status;
            AgreedOnDate = approvalDate.HasValue ? approvalDate.Value : default(DateTime?);
            return this;
        }

        public Commitment Build()
        {
            //todo: why hold a list?
            _commitment.Apprenticeships = new List<Apprenticeship>();

            foreach (var apprenticeshipBuilder in _apprenticeshipBuilders)
            {
                var apprenticeship = apprenticeshipBuilder.Build();
                _commitment.Apprenticeships.Add(apprenticeship);
            }

            DbHelper.SaveCommitment(_commitment);

            foreach (var apprenticeship in _commitment.Apprenticeships)
            {
                DbHelper.SaveApprenticeship(apprenticeship);


                foreach (var datalock in apprenticeship.DataLocks)
                {
                    DbHelper.SaveDataLock(apprenticeship, datalock);
                }

                if (apprenticeship.HasChangeOfCircumstances)
                {
                    DbHelper.CreateChangeOfCircumstances(apprenticeship);
                }
            }

            if (_commitment.TransferApprovalStatus.HasValue)
            {
                DbHelper.CreateTransferRequest(_commitment);
            }

            DbHelper.CalculatePaymentOrders(_commitment.EmployerAccountId);


            return _commitment;
        }

        public CohortBuilder WithLastAction(LastAction lastAction)
        {
            _commitment.LastAction = lastAction;
            return this;
        }

        public CohortBuilder WithCommitmentStatus(CommitmentStatus status)
        {
            _commitment.CommitmentStatus = status;
            return this;
        }

        public CohortBuilder WithLastUpdatedByProvider(string providerUser, string providerUserEmail)
        {
            _commitment.LastUpdatedByProviderName = providerUser;
            _commitment.LastUpdatedByProviderEmail = providerUserEmail;
            return this;
        }

        public CohortBuilder WithLastUpdatedByEmployer(string employerUser, string employerUserEmail)
        {
            _commitment.LastUpdatedByEmployerName = employerUser;
            _commitment.LastUpdatedByEmployerEmail = employerUserEmail;
            return this;
        }

    }
}
