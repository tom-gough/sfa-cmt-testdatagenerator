using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommitmentsDataGen.Models
{
    public enum TrainingProgrammeTypeFilter
    {
        All, Framework, Standard
    }

    public enum CommitmentStatus
    {
        New = 0,
        Active = 1,
        Deleted = 2
    }

    public enum EditStatus
    {
        Both = 0, //indicates approval by both
        Employer = 1,
        Provider = 2,
        Neither = 3 //meaningless?
    }

    public enum LastAction
    {
        None = 0,
        Amend = 1,
        Approve =2,
        AmendAfterRejected = 3
    }

    public enum TransferApprovalStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public enum AgreementStatus
    {
        NotAgreed = 0,
        EmployerAgreed = 1,
        ProviderAgreed = 2,
        BothAgreed = 3
    }

    public enum PaymentStatus
    {
        PendingApproval = 0,
        Active = 1,
        Paused = 2,
        Cancelled = 3,
        Completed = 4,
        Deleted = 5
    }

    public enum RelationshipOption
    {
        Undefined,
        Defined
    }
}
