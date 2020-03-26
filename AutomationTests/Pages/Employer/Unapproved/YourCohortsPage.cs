using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved
{
    public class YourCohortsPage : PageObject
    {
        public YourCohortsPage(Page page) : base(page)
        {
        }

        public override string PageTitle => "Your cohort requests";
        public override string Url => PageUrl;
        private static string PageUrl = Constants.EmployerBaseUrl + "/{accountId}/unapproved";

        public async Task GoToPage(string accountId)
        {
            var url = Url
                .Replace("{accountId}", accountId);

            Console.WriteLine($"Opening {url}");

            await Page.GoToAsync(url);
        }

        public async Task<T> ClickDraft<T>() where T : PageObject
        {
            return await Click<T>(Draft);
        }

        private string Draft = "#Draft";
    }
}