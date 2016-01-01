using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.RippleRest
{

    public class GetAccountSettingsResponse
    {
        public class AccountSettings
        {
            public string Account { get; set; }
            public string TransferRate { get; set; }
            public bool PasswordSpent { get; set; }
            public bool RequireDestinationTag { get; set; }
            public bool RequireAuthorization { get; set; }
            public bool DisallowXrp { get; set; }
            public bool DisableMaster { get; set; }
            public bool NoFreeze { get; set; }
            public bool GlobalFreeze { get; set; }
            public bool DefaultRipple { get; set; }
            public int TransactionSequence { get; set; }
            public string EmailHash { get; set; }
            public string WalletLocator { get; set; }
            public string WalletSize { get; set; }
            public string MessageKey { get; set; }
            public string Domain { get; set; }
            public string Signers { get; set; }
        }

        public bool Success { get; set; }
        public AccountSettings Settings { get; set; }
    }
}
