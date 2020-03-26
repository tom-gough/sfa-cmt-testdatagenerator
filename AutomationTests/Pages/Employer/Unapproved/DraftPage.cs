using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved
{
    public class DraftPage : PageObject
    {
        public DraftPage(Page page) : base(page)
        {
        }

        public override string PageTitle => "Draft apprentice details";
        public override string Url => Constants.EmployerBaseUrl + "/{accountId}/unapproved/drafts";

        public async Task GoToPage(string accountId)
        {
            var url = Url
                .Replace("{accountId}", accountId);

            Console.WriteLine($"Opening {url}");

            await Page.GoToAsync(url);
        }

        

        public async Task<int> GetNumberOfApprentices(string cohortReference)
        {
            var s = $"tr[data-cohort=\"{cohortReference}\"] td[data-label=\"Number of apprentices\"]";

            await Page.WaitForSelectorAsync(s);

            var cell = await Page.QuerySelectorAsync(s);

            var x = await cell.EvaluateFunctionAsync<string>("elements => elements.innerText");

            if (int.TryParse(x, out var result))
            {
                return result;
            }

            throw new InvalidOperationException("Unable to determine number of apprentices");
        }

    }
}