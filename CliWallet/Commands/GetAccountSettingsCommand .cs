using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliWallet;
using NDesk.Options;
using CliWallet.RippleRest;

namespace CliWallet.Commands
{
    public class GetAccountSettingsCommand : ICommand
    {
        IRippleRestClient client;
        IAccountProvider defaultAccountProvider;

        public GetAccountSettingsCommand(IRippleRestClient client, IAccountProvider defaultAccountProvider)
        {
            this.client = client;
            this.defaultAccountProvider = defaultAccountProvider;
        }

        public int Execute(string[] args, TextWriter output, TextWriter errorOutput)
        {
            var account = Helper.ParseAccountOrDefault(args, defaultAccountProvider);

            var settings = client.GetAccountSettings(account);

            output.WriteLine("Account              \t{0}", settings.Settings.Account);
            output.WriteLine("TransactionSequence  \t{0}", settings.Settings.TransactionSequence);
            output.WriteLine("DefaultRipple        \t{0}", settings.Settings.DefaultRipple);
            output.WriteLine("DisableMaster        \t{0}", settings.Settings.DisableMaster);
            output.WriteLine("DisallowXrp          \t{0}", settings.Settings.DisallowXrp);
            output.WriteLine("Domain               \t{0}", settings.Settings.Domain);
            output.WriteLine("EmailHash            \t{0}", settings.Settings.EmailHash);
            output.WriteLine("GlobalFreeze         \t{0}", settings.Settings.GlobalFreeze);
            output.WriteLine("NoFreeze             \t{0}", settings.Settings.NoFreeze);
            output.WriteLine("RequireAuthorization \t{0}", settings.Settings.RequireAuthorization);
            output.WriteLine("RequireDestinationTag\t{0}", settings.Settings.RequireDestinationTag);
            output.WriteLine("TransferRate         \t{0}", settings.Settings.TransferRate);

            return 0;
        }


    }
}


