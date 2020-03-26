using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Provider.Apprentices.ChangeEmployer
{
    public class ConfirmEmployerPage : PageObject
    {
        public ConfirmEmployerPage(Page page) : base(page)
        {
        }

        public override string PageTitle => "Choose an employer";
        public override string Url => PageUrl;
        public static string PageUrl => $"{Constants.ProviderBaseUrl}/{{providerId}}/apprentices/{{encodedApprenticeshipId}}/confirm-employer/select-employer?EmployerAccountLegalEntityPublicHashedId={{encodedAccountLegalEntityId}}";

        private string YesOption = "#confirm-true";
        private string NoOption = "#confirm-true";
        private string ContinueButton = "#saveBtn";

        public async Task ClickYes()
        {
            await Page.ClickOn(YesOption);
        }

        public async Task<T> ClickContinue<T>() where T : PageObject
        {
            var result = await Click<T>(ContinueButton);
            return result;
        }
    }
}