using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved
{
    public class CohortDetailsPage : PageObject
    {
        public CohortDetailsPage(Page page) : base(page)
        {
        }

        public override string PageTitle => ""; //Approve x apprentice details
        public override string Url => PageUrl;
        public static string PageUrl = Constants.EmployerBaseUrl + "/{accountId}/unapproved/{cohortReference}";


        public async Task GoToPage(string accountId, string cohortReference)
        {
            var url = Url
                .Replace("{accountId}", accountId)
                .Replace("{cohortReference}", cohortReference);

            Console.WriteLine($"Opening {url}");

            await Page.GoToAsync(url);
        }

        public async Task<T> ClickAddApprentice<T>() where T:PageObject
        {
            return await Click<T>(AddAnotherLink);
        }

        public async Task ClickSendToProvider()
        {
            await Page.ClickOn(SendToProvider);
        }

        public async Task<T> ClickSubmit<T>() where T : PageObject
        {
            return await Click<T>(Submit);
        }

        private static string AddAnotherLink = ".add-apprentice";
        private static string SendToProvider = "#radio-send";
        private static string Submit = ".govuk-button"; //todo: brittle



        public async Task<int> GetCohortSize()
        {
            var s = "h1[class=\"govuk-heading-xl\"]";

            await Page.WaitForSelectorAsync(s);

            var cell = await Page.QuerySelectorAsync(s);

            var x = await cell.EvaluateFunctionAsync<string>("elements => elements.innerText");

            if (int.TryParse(x, out var result))
            {
                return result;
            }

            throw new InvalidOperationException("Unable to determine cohort size");
        }

    }


}
