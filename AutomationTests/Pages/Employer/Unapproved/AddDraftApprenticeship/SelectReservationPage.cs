using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Employer.Unapproved.AddDraftApprenticeship
{
    public class SelectReservationPage : PageObject
    {
        private static string ContinueButton = ".btn-success";

        public override string PageTitle => "Reservations"; //stub, so doesn't matter
        public override string Url => "";
        public static string PageUrl = PageUrl;

        public SelectReservationPage(Page page) : base(page)
        {
  
        }

        public async Task<T> ClickContinue<T>() where T : PageObject
        {
            return await Click<T>(ContinueButton);
        }
    }
}
