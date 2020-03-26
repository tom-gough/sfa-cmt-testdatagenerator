namespace AutomationTests.Actors
{
    public class Provider
    {
        public long ProviderId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public static Provider Create()
        {
            return new Provider
            {
                Username = "oattestuser+10005077@gmail.com",
                Password = "2lineHUNTDATE+",
                ProviderId = 10005077
            };
        }
    }
}
