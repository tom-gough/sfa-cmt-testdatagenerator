using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddDraftApprenticeship
{
    public class AddDraftApprenticeshipPage : PageObject
    {
        public AddDraftApprenticeshipPage(Page page) : base(page)
        {
        }

        public override string PageTitle => "Add apprentice details";
        public override string Url => PageUrl;
        public static string PageUrl = Constants.EmployerBaseUrl + "/{accountId}/unapproved/{cohortReference}/add";
        
        public async Task<T> ClickContinue<T>() where T: PageObject
        {
            return await Click<T>(ContinueButton);
        }

        public async Task EnterFirstName(string value)
        {
            await Page.TypeInputAsync(FirstName, value);
        }

        public async Task EnterLastName(string value)
        {
            await Page.TypeInputAsync(LastName, value);
        }

        private static string FirstName = "#FirstName";
        private static string LastName = "#LastName";
        private static string ContinueButton = ".govuk-button";
    }
}
