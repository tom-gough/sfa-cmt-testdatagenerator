using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Provider.Apprentices
{
    public class ApprenticeDetailsPage : PageObject
    {
        public ApprenticeDetailsPage(Page page) : base(page)
        {
        }

        public override string PageTitle => ""; //todo: check page title
        public override string Url => PageUrl;
        private static readonly string PageUrl = Constants.ProviderBaseUrl + "/{providerId}/apprentices/{encodedApprenticeshipId}";

        private string ChangeEmployerLink = "#change-employer-link";

        public async Task<T> ClickChangeEmployer<T>() where T : PageObject
        {
            var result = await Click<T>(ChangeEmployerLink);
            return result;
        }

        public async Task GoToPage(long providerId, string encodedApprenticeshipId)
        {
            var url = Url.Replace("{providerId}", providerId.ToString());
            url = url.Replace("{encodedApprenticeshipId}", encodedApprenticeshipId);
            Console.WriteLine($"Opening {url}");
            await Page.GoToAsync(url);
        }

    }
}
