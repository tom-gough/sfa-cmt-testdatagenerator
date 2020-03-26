using System;
using System.Threading.Tasks;
using AutomationTests.Pages.Provider.Apprentices;
using AutomationTests.Pages.Provider.Apprentices.ChangeEmployer;
using NUnit.Framework;


namespace AutomationTests.Provider.Apprentices
{
    [TestFixture]
    public class ChangeEmployerTests : ProviderTestBase
    {
        [Test]
        public async Task TestChangeEmployerJourney()
        {
            ScenarioBuilder.Generator.Automation.Generator.Single_Stopped_Apprentice();
            var encodedApprenticeshipId = "JRML7V"; //todo: get this from scenario, or from db?

            var provider = Actors.Provider.Create();

            var apprenticeDetails = new ApprenticeDetailsPage(Page);
            await apprenticeDetails.GoToPage(provider.ProviderId, encodedApprenticeshipId);

            var inform = await apprenticeDetails.ClickChangeEmployer<InformPage>();
            var selectEmployer = await inform.ClickContinue<SelectEmployerPage>();
            var confirmEmployer = await selectEmployer.ClickSelectEmployer<ConfirmEmployerPage>();
            await confirmEmployer.ClickYes();
            var newStartDate = await confirmEmployer.ClickContinue<NewStartDatePage>();

            await newStartDate.EnterMonth("03");
            await newStartDate.EnterYear("2020");
            var newPrice = await newStartDate.ClickContinue<NewPricePage>();

            await newPrice.EnterPrice("4000");
            var confirmPage = await newPrice.ClickContinue<ConfirmPage>();

            var sent = await confirmPage.ClickContinue<SentPage>();

        }
    }
}
