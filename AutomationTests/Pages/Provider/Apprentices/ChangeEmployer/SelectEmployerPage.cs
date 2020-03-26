using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Provider.Apprentices.ChangeEmployer
{
    public class SelectEmployerPage : PageObject
    {
        public SelectEmployerPage(Page page) : base(page)
        {
        }

        public override string PageTitle => "Choose an employer";
        public override string Url => PageUrl;
        public static string PageUrl => $"{Constants.ProviderBaseUrl}/{{providerId}}/apprentices/{{encodedApprenticeshipId}}/change-employer/select-employer";

        private string SelectEmployerLink = ".govuk-table__cell--numeric>a";

        public async Task<T> ClickSelectEmployer<T>() where T : PageObject
        {
            var result = await Click<T>(SelectEmployerLink);
            return result;
        }
    }

    public class NewStartDatePage : PageObject
    {
        public NewStartDatePage(Page page) : base(page)
        {
        }

        public override string PageTitle => "New training start date";
        public override string Url => PageUrl;
        public static string PageUrl => $"{Constants.ProviderBaseUrl}/{{providerId}}/apprentices/{{encodedApprenticeshipId}}/change-employer/dates";

        private string Month = "#start-date-month";
        private string Year = "#start-date-year";
        private string ContinueButton = "#save-and-continue-button";

        public async Task EnterMonth(string value)
        {
            await Page.TypeInputAsync(Month, value);
        }

        public async Task EnterYear(string value)
        {
            await Page.TypeInputAsync(Year, value);
        }

        public async Task<T> ClickContinue<T>() where T : PageObject
        {
            var result = await Click<T>(ContinueButton);
            return result;
        }
    }

    public class NewPricePage : PageObject
    {
        public NewPricePage(Page page) : base(page)
        {
        }

        public override string PageTitle => "What's the new agreed apprenticeship price?";
        public override string Url => PageUrl;
        public static string PageUrl => $"{Constants.ProviderBaseUrl}/{{providerId}}/apprentices/{{encodedApprenticeshipId}}/change-employer/price";

        private string Price = "#Price";
        private string ContinueButton = "#save-and-continue-button";

        public async Task EnterPrice(string value)
        {
            await Page.TypeInputAsync(Price, value);
        }

        public async Task<T> ClickContinue<T>() where T : PageObject
        {
            var result = await Click<T>(ContinueButton);
            return result;
        }
    }

    public class ConfirmPage : PageObject
    {
        public ConfirmPage(Page page) : base(page)
        {
        }

        public override string PageTitle => "Confirm the information before sending your request";
        public override string Url => PageUrl;
        public static string PageUrl => $"{Constants.ProviderBaseUrl}/{{providerId}}/apprentices/{{encodedApprenticeshipId}}/change-employer/confirm";

        private string ContinueButton = "#confirm-button";

        public async Task<T> ClickContinue<T>() where T : PageObject
        {
            var result = await Click<T>(ContinueButton);
            return result;
        }
    }


    public class SentPage : PageObject
    {
        public SentPage(Page page) : base(page)
        {
        }

        public override string PageTitle => "Change of employer requested";
        public override string Url => PageUrl;
        public static string PageUrl => $"{Constants.ProviderBaseUrl}/{{providerId}}/apprentices/{{encodedApprenticeshipId}}/change-employer/sent";
    }

}