using System.Threading.Tasks;
using AutomationTests.Actors;
using AutomationTests.Pages.Employer.Unapproved;
using AutomationTests.Pages.Employer.Unapproved.AddDraftApprenticeship;
using NUnit.Framework;

namespace AutomationTests.Employer.DraftApprenticeship
{
    [TestFixture]
    public class AddDraftApprenticeshipTests : EmployerTestBase
    {
        [TestCase(EmployerActor.NonLevyEmployer)]
        [TestCase(EmployerActor.LevyEmployer)]
        public async Task AddDetails(EmployerActor employerActor)
        {
            const int cohortSize = 10;

            var employer = Actors.Employer.Create(employerActor);

            ScenarioBuilder.Generator.Automation.Generator.Cohort_Employer_Draft(cohortSize, !employer.IsLevyPayer);

            var cohortReference = "JRML7V"; //todo : get this, or something

            var page = new CohortDetailsPage(Page);
            await page.GoToPage(employer.EncodedAccountId, cohortReference);

            AddDraftApprenticeshipPage addPage;

            //Non-levy payers (TODO: also transfer receivers!) have to select a reservation too
            if (!employer.IsLevyPayer)
            {
                var selectReservationPage = await page.ClickAddApprentice<SelectReservationPage>();
                addPage = await selectReservationPage.ClickContinue<AddDraftApprenticeshipPage>();
            }
            else
            {
                addPage = await page.ClickAddApprentice<AddDraftApprenticeshipPage>();
            }

            await addPage.EnterFirstName("Jed");
            await addPage.EnterLastName("Springfield");

            var detailsPage = await addPage.ClickContinue<CohortDetailsPage>();

            //todo: this doesn't work for some reason.
            //var actual = detailsPage.GetCohortSize();
            //Assert.AreEqual(cohortSize + 1, actual);

            //todo: make some assertions now
        }
    }
}
