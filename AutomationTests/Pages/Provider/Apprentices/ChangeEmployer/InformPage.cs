using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Provider.Apprentices.ChangeEmployer
{
    public class InformPage : PageObject
    {
        public InformPage(Page page) : base(page)
        {
        }

        public override string PageTitle => "What you'll need";
        public override string Url => PageUrl;
        public static string PageUrl => $"{Constants.ProviderBaseUrl}/{{providerId}}/apprentices/{{encodedApprenticeshipId}}/change-employer";

        private string ContinueButton = ".govuk-button";

        public async Task<T> ClickContinue<T>() where T : PageObject
        {
            var result = await Click<T>(ContinueButton);
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
