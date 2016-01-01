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
    public class GetBalanceCommand : ICommand
    {
        IRippleRestClient client;
        IAccountProvider defaultAccountProvider;

        public GetBalanceCommand(IRippleRestClient client, IAccountProvider defaultAccountProvider)
        {
            this.client = client;
            this.defaultAccountProvider = defaultAccountProvider;
        }

        public int Execute(string[] args, TextWriter output, TextWriter errorOutput)
        {
            var account = Helper.ParseAccountOrDefault(args, defaultAccountProvider);

            var balances = client.GetAccountBalances(account);

            foreach (var b in balances.Balances)
            {
                var formattedValue = string.Format(CultureInfo.InvariantCulture, "{0,12:0.##}", b.Value);
                output.WriteLine($"{formattedValue}\t{b.Currency.PadLeft(3)}\t{b.Counterparty}");
            }

            return 0;
        }
    }
}

