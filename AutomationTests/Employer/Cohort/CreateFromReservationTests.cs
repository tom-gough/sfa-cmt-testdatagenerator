using System.Threading.Tasks;
using AutomationTests.Actors;
using AutomationTests.Pages.Employer.Unapproved;
using AutomationTests.Pages.Employer.Unapproved.AddCohort;
using NUnit.Framework;

namespace AutomationTests.Employer.Cohort
{
    [TestFixture]
    public class CreateCohortFromReservationTests : EmployerTestBase
    {
        [TestCase(EmployerActor.NonLevyEmployer)]
        public async Task CreateCohort(EmployerActor employerActor)
        {
            var employer = Actors.Employer.Create(employerActor);

            //Start on UseReservations
            var useReservationPage = new UseReservationPage(Page);
            await useReservationPage.GoToPage(employer.EncodedAccountId);
            var startPage = await useReservationPage.ClickContinue<AddApprenticeStartPage>();

            //Start page
            var selectProviderPage = await startPage.ClickContinue<SelectProviderPage>();

            //select/add provider
            await selectProviderPage.EnterProviderId(DefaultProvider.ProviderId.ToString());
            var confirmProvider = await selectProviderPage.ClickContinue<ConfirmProviderPage>();

            //confirm provider
            await confirmProvider.SelectConfirmationOption();
            var assign = await confirmProvider.ClickContinue();

            //Assign
            await assign.SelectIWillAddApprentices();
            var apprentice = await assign.ClickContinue<ApprenticePage>();

            //Apprentice
            await apprentice.EnterFirstName("Chris");
            await apprentice.EnterLastName("Foster");
            
            var cohortDetails = await apprentice.ClickContinue<CohortDetailsPage>();

            //Assertions

            await cohortDetails.ClickSendToProvider();
            var sentPage = await cohortDetails.ClickSubmit<SentPage>();
        }


        [TestCase(EmployerActor.NonLevyEmployer)]
        public async Task CreateCohortWithProvider(EmployerActor employerActor)
        {
            var employer = Actors.Employer.Create(employerActor);

            //Start on UseReservations
            var useReservationPage = new UseReservationPage(Page);
            await useReservationPage.GoToPage(employer.EncodedAccountId);
            var startPage = await useReservationPage.ClickContinue<AddApprenticeStartPage>();

            //Start page
            var selectProviderPage = await startPage.ClickContinue<SelectProviderPage>();

            //select/add provider
            await selectProviderPage.EnterProviderId(DefaultProvider.ProviderId.ToString());
            var confirmProvider = await selectProviderPage.ClickContinue<ConfirmProviderPage>();

            //confirm provider
            await confirmProvider.SelectConfirmationOption();
            var assign = await confirmProvider.ClickContinue();

            //Assign
            await assign.SelectProviderWillAddApprentices();
            var message = await assign.ClickContinue<MessagePage>();

            //Apprentice
            await message.EnterMessage("Hey there!");
            await message.ClickSend();

            //Assertions
        }

        [TestCase(EmployerActor.NonLevyEmployer)]
        public async Task GoBack(EmployerActor employerActor)
        {
            var employer = Actors.Employer.Create(employerActor);

            //Start on UseReservations
            var useReservationPage = new UseReservationPage(Page);
            await useReservationPage.GoToPage(employer.EncodedAccountId);
            var startPage = await useReservationPage.ClickContinue<AddApprenticeStartPage>();

            //Start page
            var selectProviderPage = await startPage.ClickContinue<SelectProviderPage>();

            //select/add provider
            await selectProviderPage.EnterProviderId(DefaultProvider.ProviderId.ToString());
            var confirmProvider = await selectProviderPage.ClickContinue<ConfirmProviderPage>();

            //confirm provider
            await confirmProvider.SelectConfirmationOption();
            var assign = await confirmProvider.ClickContinue();

            //Assign
            await assign.SelectIWillAddApprentices();
            var apprentice = await assign.ClickContinue<ApprenticePage>();

            //Now we go back
            var back1 = await apprentice.ClickBack<AssignPage>();
            var back2 = await back1.ClickBack<ConfirmProviderPage>();
            var back3 = await back2.ClickBack<SelectProviderPage>();
            var back4 = await back3.ClickBack<AddApprenticeStartPage>();
            var back5 = await back4.ClickBack<UseReservationPage>();

        }
    }
}