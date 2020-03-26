using System.Threading.Tasks;
using AutomationTests.Actors;
using AutomationTests.Pages.Employer.Unapproved;
using NUnit.Framework;

namespace AutomationTests.Employer.Landing
{
    [TestFixture]
    public class LandingTests : EmployerTestBase
    {
        [TestCase(EmployerActor.NonLevyEmployer)]
        [TestCase(EmployerActor.LevyEmployer)]
        public async Task CreateCohortFromHomePage(EmployerActor employerActor)
        {
            ScenarioBuilder.Generator.Generator.Scenario_Cohort_NonLevyEmployer_ReadyForApproval();

            var employer = Actors.Employer.Create(employerActor);

            var landingPage = await LandingPage.Get(Page, employer.EncodedAccountId);
        }
    }
}