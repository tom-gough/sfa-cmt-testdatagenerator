using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommitmentsDataGen.Models;
using Dapper;
using Newtonsoft.Json;

namespace CommitmentsDataGen.Helpers
{
    public static class DbHelper
    {
        private static string DbConnectionString { get; set; }

        private static DbConnection GetConnection()
        {
            if (String.IsNullOrEmpty(DbConnectionString))
            {
                var settings = new System.Configuration.AppSettingsReader();
                DbConnectionString = (string)settings.GetValue("DbConnectionString", typeof(string));
            }

            return new SqlConnection(DbConnectionString);
        }

        public static void ClearDb()
        {
            var connection = GetConnection();

            connection.Execute("delete from DataLockStatus");
            connection.Execute("delete from ApprenticeshipUpdate");
            connection.Execute("delete from TransferRequest");
            connection.Execute("delete from CustomProviderPaymentPriority");
            connection.Execute("delete from History");
            connection.Execute("delete from Relationship"); 
            connection.Execute("delete from PriceHistory");
            connection.Execute("delete from Message");
            connection.Execute("delete from Apprenticeship");
            connection.Execute("delete from Commitment");

            connection.Execute("DBCC CHECKIDENT ('[Commitment]', RESEED, 0);");
            connection.Execute("DBCC CHECKIDENT ('[Apprenticeship]', RESEED, 0);");
        }

        

        public static void SaveCommitment(Commitment cohort)
        {
            var connection = GetConnection();

            var query = ("insert into Commitment " +
                               "([Reference],[EmployerAccountId] ,[LegalEntityId],[LegalEntityName],[LegalEntityAddress]" +
                               ",[LegalEntityOrganisationType],[ProviderId],[ProviderName],[CommitmentStatus],[EditStatus]" +
                               ",[CreatedOn],[LastAction],[LastUpdatedByEmployerName],[LastUpdatedByEmployerEmail],[LastUpdatedByProviderName]" +
                               ",[LastUpdatedByProviderEmail],[TransferSenderId],[TransferSenderName],[TransferApprovalStatus]" +
                               ",[TransferApprovalActionedByEmployerName]," +
                               "[TransferApprovalActionedByEmployerEmail],[TransferApprovalActionedOn]) " +
                               "VALUES(" +
                                 "@Reference,@EmployerAccountId ,@LegalEntityId,@LegalEntityName,@LegalEntityAddress" +
                                 ",@LegalEntityOrganisationType,@ProviderId,@ProviderName,@CommitmentStatus,@EditStatus" +
                                 ",@CreatedOn,@LastAction,@LastUpdatedByEmployerName,@LastUpdatedByEmployerEmail,@LastUpdatedByProviderName" +
                                 ",@LastUpdatedByProviderEmail,@TransferSenderId,@TransferSenderName,@TransferApprovalStatus" +
                                 ",@TransferApprovalActionedByEmployerName," +
                                 "@TransferApprovalActionedByEmployerEmail,@TransferApprovalActionedOn" +
                                 ")"
                               );

            var result = connection.Execute(query, cohort);
        }

        public static void SaveApprenticeship(Apprenticeship apprenticeship)
        {
            var connection = GetConnection();

            var query = "insert into Apprenticeship " +
                        "([CommitmentId],[FirstName],[LastName],[ULN],[TrainingType],[TrainingCode],[TrainingName],[Cost]" +
                        ",[StartDate],[EndDate],[AgreementStatus],[PaymentStatus],[DateOfBirth],[NINumber],[EmployerRef]" +
                        ",[ProviderRef],[CreatedOn],[AgreedOn],[PaymentOrder],[StopDate],[PauseDate],[HasHadDataLockSuccess])" +
                        "VALUES(" +
                        "@CommitmentId,@FirstName,@LastName, @ULN, @TrainingType, @TrainingCode,@TrainingName, @Cost,@StartDate," +
                        "@EndDate,@AgreementStatus,@PaymentStatus,@DateOfBirth,@NINumber, @EmployerRef, @ProviderRef, @CreatedOn, @AgreedOn, " +
                        "@PaymentOrder, @StopDate, @PauseDate, @HasHadDataLockSuccess)";

            var result = connection.Execute(query, apprenticeship);
        }

        public static void CreateEmployerProviderRelationship(Commitment commitment, RelationshipOption verified)
        {
            if (HasEmployerProviderRelationship(commitment))
            {
                return;
            }

            var connection = GetConnection();

            var query = "insert into Relationship " +
                        "([ProviderId],[ProviderName],[EmployerAccountId],[LegalEntityId],[LegalEntityName],[LegalEntityAddress],[LegalEntityOrganisationType],[Verified],[CreatedOn])" +
                        "VALUES(" +
                        "@ProviderId,@ProviderName,@EmployerAccountId, @LegalEntityId, @LegalEntityName, @LegalEntityAddress, @LegalEntityOrganisationType, @Verified, @CreatedOn)";

            var result = connection.Execute(query, new
            {
                ProviderId = commitment.ProviderId,
                ProviderName = commitment.ProviderName,
                EmployerAccountId = commitment.EmployerAccountId,
                LegalEntityId = commitment.LegalEntityId,
                LegalEntityName = commitment.LegalEntityName,
                LegalEntityAddress = commitment.LegalEntityAddress,
                LegalEntityOrganisationType = commitment.LegalEntityOrganisationType,
                Verified = verified == RelationshipOption.Undefined ? default(bool?) : true,
                CreatedOn = DateTime.Now
            });
        }

        public static bool HasEmployerProviderRelationship(Commitment commitment)
        {
            var connection = GetConnection();
            var query =
                "select top 1 1 from Relationship where ProviderId=@ProviderId and EmployerAccountId=@EmployerAccountId";

            var result = connection.Query<bool>(query, new
            {
                ProviderId = commitment.ProviderId,
                EmployerAccountId = commitment.EmployerAccountId
            }).FirstOrDefault();

            return result;
        }

        public static void CreateTransferRequest(Commitment commitment)
        {
            var connection = GetConnection();

            var query = "insert into TransferRequest " +
                         "(CommitmentId, TrainingCourses, Cost, [Status], [TransferApprovalActionedByEmployerName]," +
                        "[TransferApprovalActionedByEmployerEmail],[TransferApprovalActionedOn],[CreatedOn])" +
                         "VALUES(@CommitmentId, @TrainingCourses, @Cost, @Status, @TransferApprovalActionedByEmployerName," +
                        "@TransferApprovalActionedByEmployerEmail,@TransferApprovalActionedOn,@CreatedOn" + ")";

            var courseGroups = commitment.Apprenticeships.GroupBy(x => x.TrainingName);

            var courses = courseGroups.Select(x => new
            {
                CourseTitle = x.Key,
                ApprenticeshipCount = x.Count()
            });

            var courseText = JsonConvert.SerializeObject(courses);

            var result = connection.Execute(query, new
            {
                CommitmentId = commitment.Id,
                TrainingCourses = courseText,
                Cost = commitment.Apprenticeships.Sum(x => x.Cost),
                Status = commitment.TransferApprovalStatus,
                TransferApprovalActionedByEmployerName = "Person",
                TransferApprovalActionedByEmployerEmail = "person@mail.com",
                TransferApprovalActionedOn = default(DateTime?),
                CreatedOn = DateTime.Now
            });
        }
    }
}
