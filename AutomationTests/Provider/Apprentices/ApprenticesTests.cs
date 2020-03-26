using System.Threading.Tasks;
using AutomationTests.Pages.Provider.Apprentices;
using NUnit.Framework;

namespace AutomationTests.Provider.Apprentices
{
    [TestFixture]
    public class ApprenticesTests : ProviderTestBase
    {
        [Test]
        public async Task TestPagination_NextPage()
        {
            ScenarioBuilder.Generator.Generator.ManyApprovedNonLevy();
            var provider = Actors.Provider.Create();

            var expectedPageCount = 5;
            var pageCount = 1;
            
            var manageApprenticesPage = new ManageApprenticesPage(Page);
            await manageApprenticesPage.GoToPage(provider.ProviderId);

            var hasNextPageLink = await manageApprenticesPage.HasNextPageLink();

            while (hasNextPageLink)
            {
                manageApprenticesPage = await manageApprenticesPage.ClickNextPage<ManageApprenticesPage>();
                pageCount++;
                hasNextPageLink = await manageApprenticesPage.HasNextPageLink();
            }

            Assert.AreEqual(expectedPageCount, pageCount);
        }
    }
}
