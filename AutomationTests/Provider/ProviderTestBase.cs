using System;
using System.Threading.Tasks;
using AutomationTests.Pages.Provider;
using NUnit.Framework;
using PuppeteerSharp;
using ScenarioBuilder.Helpers;

namespace AutomationTests.Provider
{
    public class ProviderTestBase
    {
        protected Browser Browser { get; set; }
        protected Page Page { get; set; }

        [SetUp]
        public async Task Setup()
        {
            ConfigurationHelper.Initialise(TestContext.CurrentContext.TestDirectory);
            DbHelper.ClearDb();
            IdentityHelpers.ClearAll();

            //todo: allow other actors (types of provider)
            //var providerActor = (ProviderActor)TestContext.CurrentContext.Test.Arguments.GetValue(0);
            var provider = Actors.Provider.Create();

            
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

            var launchOptions = new LaunchOptions
            {
                Headless = false,
                IgnoreHTTPSErrors = true,
                //SlowMo = 10
            };

            Console.WriteLine("Launching browser...");

            Browser = await Puppeteer.LaunchAsync(launchOptions);

            var context = Browser.DefaultContext;

            Page = await context.NewPageAsync();

            var url = LaunchPage.Url.Replace("{providerId}", provider.ProviderId.ToString());
            Console.WriteLine($"Opening {url}");
            var response = await Page.GoToAsync(url);

            //Realm selection
            await Page.ClickOn(LaunchPage.Realm);
            await Page.WaitForNavigationAsync();
            await Page.TypeInputAsync(LaunchPage.Username, provider.Username);
            await Page.TypeInputAsync(LaunchPage.Password, provider.Password);
            Console.Write("Signing in... ");
            await Page.Keyboard.DownAsync("Enter"); //hit enter, rather than click button
            await Page.WaitForSelectorAsync(".govuk-header");
            //await Page.WaitForNavigationAsync();
            Console.WriteLine("Complete");
        }

        [TearDown]
        public async Task TearDown()
        {
            await Page.CloseAsync();
            await Browser.CloseAsync();
        }
    }
}
