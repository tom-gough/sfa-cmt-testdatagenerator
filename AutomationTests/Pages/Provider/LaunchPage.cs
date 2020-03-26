namespace AutomationTests.Pages.Provider
{
    public class LaunchPage
    {
        public static string Url = Constants.ProviderBaseUrl + "/{providerId}/apprentices";
        public static string Username = "#username";
        public static string Password = "#password";
        public static string Realm = ".idp[tabindex=\"4\"]";
    }
}
