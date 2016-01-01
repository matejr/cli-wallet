using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.Configuration
{
    class StaticDefaultAccountAndSecretProvider : IAccountAndSecretProvider
    {
        public string Account { get; }
        public string Secret { get; }

        public StaticDefaultAccountAndSecretProvider(string account, string secret)
        {
            Account = account;
            Secret = secret;
        }
    }
}
