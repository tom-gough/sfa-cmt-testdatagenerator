using System.Linq;
using HashidsNet;

namespace ScenarioBuilder.Helpers
{
    public static class HashingHelper
    {
        private static readonly Hashids HashGenerator;
        private static readonly Hashids AccountLegalEntityHashGenerator;

        static HashingHelper()
        {
            var config = ConfigurationHelper.Configuration;
            HashGenerator = new Hashids(config.HashSalt, 6,config.HashAlphabet);
            AccountLegalEntityHashGenerator = new Hashids(config.AccountLegalEntityHashSalt, 6, config.AccountLegalEntityHashAlphabet);
        }

        public static string Encode(long id)
        {
            return HashGenerator.EncodeLong(id);
        }

        public static long Decode(string hash)
        {
            return HashGenerator.DecodeLong(hash).First();
        }

        public static string EncodeAccountLegalEntityId(long id)
        {
            return AccountLegalEntityHashGenerator.EncodeLong(id);
        }
        public static long DecodeAccountLegalEntityId(string hash)
        {
            return AccountLegalEntityHashGenerator.DecodeLong(hash).First();
        }

    }
}
