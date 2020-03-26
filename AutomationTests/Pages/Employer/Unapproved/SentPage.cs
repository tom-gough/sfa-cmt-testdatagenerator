using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved
{
    public class SentPage : PageObject
    {
        public SentPage(Page page) : base(page)
        {
        }

        public override string PageTitle => "Notification sent to training provider";
        public override string Url => PageUrl;
        private static readonly string PageUrl = Constants.EmployerBaseUrl + "/{accountId}/unapproved/{cohortReference}/sent";

        public async Task GoToPage(string accountId, string cohortReference)
        {
            var url = PageUrl
                .Replace("{accountId}", accountId)
                .Replace("{cohortReference}", cohortReference);

            Console.WriteLine($"Opening {url}");

            await Page.GoToAsync(url);
        }
    }
}