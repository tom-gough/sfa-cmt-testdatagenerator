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
        public RelationshipOption EmployerProviderRelationshipExists { get; private set; }

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
                LastUpdatedByProviderEmail = "",
                AccountLegalEntityPublicHashedId = "XYZ" + id
            };

            _apprenticeshipBuilders = new List<ApprenticeshipBuilder>();
        }

        public CohortBuilder WithDefaultEmployerProvider(RelationshipOption option)
        {
            _commitment.EmployerAccountId = 8194;
            _commitment.LegalEntityId = "06344082";
            _commitment.LegalEntityName = "ASAP CATERING LIMITED (Stub)";
            _commitment.LegalEntityAddress = "18 Soho Square, London, W1D 3QL";
            _commitment.LegalEntityOrganisationType = 1;
            _commitment.ProviderId = 10005124;
            _commitment.ProviderName = "PLUMPTON COLLEGE";

            EmployerProviderRelationshipExists = option;

            return this;
        }

        public CohortBuilder WithDefaultEmployer()
        {
            _commitment.EmployerAccountId = 8194;
            _commitment.LegalEntityId = "06344082";
            _commitment.LegalEntityName = "ASAP CATERING LIMITED (Stub)";
            _commitment.LegalEntityAddress = "18 Soho Square, London, W1D 3QL";
            _commitment.LegalEntityOrganisationType = 1;
            return this;
        }

        public CohortBuilder WithDefaultProvider(RelationshipOption option)
        {
            _commitment.ProviderId = 10005124;
            _commitment.ProviderName = "PLUMPTON COLLEGE";

            EmployerProviderRelationshipExists = option;

            return this;
        }

        public CohortBuilder WithEmployer(long accountId, string legalEntityId, string name)
        {
            _commitment.EmployerAccountId = accountId;
            _commitment.LegalEntityId = legalEntityId;
            _commitment.LegalEntityName = name;
            _commitment.LegalEntityAddress = "Some address";
            _commitment.LegalEntityOrganisationType = 1;
            return this;
        }

        public CohortBuilder WithProvider(int providerId, string providerName, RelationshipOption option)
        {
            _commitment.ProviderId = providerId;
            _commitment.ProviderName = providerName;

            EmployerProviderRelationshipExists = option;

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

        public CohortBuilder WithApprenticeshipPaymentStatus(PaymentStatus status)
        {
            PaymentStatus = status;
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
            }

            DbHelper.CreateEmployerProviderRelationship(_commitment, EmployerProviderRelationshipExists);

            if (_commitment.TransferApprovalStatus.HasValue)
            {
                DbHelper.CreateTransferRequest(_commitment);
            }

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
