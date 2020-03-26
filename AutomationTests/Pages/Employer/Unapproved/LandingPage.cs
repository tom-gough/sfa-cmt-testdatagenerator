using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved
{
    public class LandingPage : PageObject
    {
        public LandingPage(Page page) : base(page)
        {
        }

        public override string PageTitle => "Apprentices";
        public override string Url => PageUrl;
        private static string PageUrl = Constants.EmployerBaseUrl + "/{accountId}";

        public static async Task<LandingPage> Get(Page page, string accountId)
        {
            var result = new LandingPage(page);
            await result.GoToPage(accountId);
            return result;
        }

        private async Task GoToPage(string accountId)
        {
            var url = Url
                .Replace("{accountId}", accountId);

            Console.WriteLine($"Opening {url}");

            await Page.GoToAsync(url);
        }

        public async Task<T> ClickYourCohorts<T>() where T : PageObject
        {
            await Page.ClickOn(YourCohorts);
            return PageObjectFactory.CreatePage<T>(Page) as T;
        }

        private static string AddApprentice = "#AddApprentice";
        private static string YourCohorts = "#YourCohorts";
    }
}
