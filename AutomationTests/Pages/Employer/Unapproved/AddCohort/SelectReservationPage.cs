using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddCohort
{
    public class SelectReservationPage : PageObject
    {
        public SelectReservationPage(Page page): base(page)
        {
        }

        public override string PageTitle => "Reservations";
        public override string Url => PageUrl;
        public static string PageUrl = "https://sfa-stub-reservations.herokuapp.com/accounts/{accountId}/reservations";

        public async Task<T> ClickContinue<T>() where T: PageObject
        {
            return await Click<T>(ContinueButton);
        }

        private static string ContinueButton = ".btn-success";
    }
}
