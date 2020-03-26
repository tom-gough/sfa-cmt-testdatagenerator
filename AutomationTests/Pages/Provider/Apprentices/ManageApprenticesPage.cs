using System;
using System.Linq;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace AutomationTests.Pages.Provider.Apprentices
{
    public class ManageApprenticesPage : PageObject
    {
        public ManageApprenticesPage(Page page) : base(page)
        {
        }

        public override string PageTitle => "Manage Your Apprentices";
        public override string Url => PageUrl;
        private static readonly string PageUrl = Constants.ProviderBaseUrl + "/{providerId}/apprentices";

        private string ApplyFilters = ".govuk-button .govuk-!-margin-0";
        private string SearchButton = ".das-search-form__button";
        private string NextPage = ".das-pagination__link[aria-label=\"Next page\"]";

        public async Task<T> ClickNextPage<T>() where T : PageObject
        {
            var result = await Click<T>(NextPage);
            return result;
        }

        public async Task<bool> HasNextPageLink()
        {
            await Page.WaitForSelectorAsync(".das-pagination");
            var result = await Page.QuerySelectorAllAsync(NextPage);
            return result.Any();
        }

        public async Task GoToPage(long providerId)
        {
            var url = Url.Replace("{providerId}", providerId.ToString());
            Console.WriteLine($"Opening {url}");
            await Page.GoToAsync(url);
        }
    }
}
