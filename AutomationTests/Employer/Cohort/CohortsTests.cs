using System.Threading.Tasks;
using AutomationTests.Actors;
using AutomationTests.Pages.Employer.Unapproved;
using NUnit.Framework;
using ScenarioBuilder.Helpers;

namespace AutomationTests.Employer.Cohort
{
    [TestFixture]
    public class CohortTests : EmployerTestBase
    {
        [TestCase(EmployerActor.NonLevyEmployer)]
        [TestCase(EmployerActor.LevyEmployer)]
        public async Task Draft_Cohorts(EmployerActor employerActor)
        {
            var cohortSize = RandomHelper.GetRandomNumber(9) + 1;

            ScenarioBuilder.Generator.Automation.Generator.Cohort_Employer_Draft(cohortSize, employerActor == EmployerActor.NonLevyEmployer);

            var employer = Actors.Employer.Create(employerActor);

            //get landing page
            var landingPage = await LandingPage.Get(Page, employer.EncodedAccountId);
            var cohorts = await landingPage.ClickYourCohorts<YourCohortsPage>();

            var draft = await cohorts.ClickDraft<DraftPage>();
            var actualNumberOfApprentices = await draft.GetNumberOfApprentices("JRML7V"); //todo: capture cohort reference from scenario?

            Assert.AreEqual(cohortSize, actualNumberOfApprentices);
        }


        [TestCase(EmployerActor.NonLevyEmployer)]
        [TestCase(EmployerActor.LevyEmployer)]
        public async Task Draft_Cohorts_With_Provider_Should_Not_Be_Present(EmployerActor employerActor)
        {
            var cohortSize = RandomHelper.GetRandomNumber(9) + 1;

            ScenarioBuilder.Generator.Automation.Generator.Cohort_Provider_Draft(cohortSize, employerActor == EmployerActor.NonLevyEmployer);

            var employer = Actors.Employer.Create(employerActor);

            //get landing page
            var landingPage = await LandingPage.Get(Page, employer.EncodedAccountId);
            var cohorts = await landingPage.ClickYourCohorts<YourCohortsPage>();

            //todo: get count and assert zero (all)
        }
    }
}
