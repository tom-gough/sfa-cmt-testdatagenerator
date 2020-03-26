using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddCohort
{
    public class UseReservationPage : PageObject
    {
        public UseReservationPage(Page page) : base(page)
        {
        }

        public override string PageTitle => "Reservations"; //stub, so doesn't matter
        public override string Url => PageUrl;
        public static string PageUrl = "https://sfa-stub-reservations.herokuapp.com/accounts/{accountId}/reservations";


        public async Task GoToPage(string accountId)
        {
            var url = Url.Replace("{accountId}", accountId);
            await Page.GoToAsync(url);
        }
        
        private static string ContinueButton = ".btn-success";

        public async Task<T> ClickContinue<T>() where T: PageObject
        {
            return await Click<T>(ContinueButton);
        }
    }
}
