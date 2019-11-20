using System.Linq;
using HashidsNet;

namespace ScenarioBuilder.Helpers
{
    public static class HashingHelper
    {
        private static readonly Hashids HashGenerator;

        static HashingHelper()
        {
            var salt = ConfigurationHelper.Configuration.HashSalt;
            var hashAlphabet = ConfigurationHelper.Configuration.HashAlphabet;

            HashGenerator = new Hashids(salt, 6, hashAlphabet);
        }

        public static string Encode(long id)
        {
            return HashGenerator.EncodeLong(id);
        }

        public static long Decode(string hash)
        {
            return HashGenerator.DecodeLong(hash).First();
        }
    }
}
