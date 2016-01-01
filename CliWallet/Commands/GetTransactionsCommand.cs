using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliWallet;
using CliWallet.RippleRest;

namespace CliWallet.Commands
{
    public class GetTransactionsCommand : ICommand
    {
        IRippleRestClient client;
        IAccountAndPasswordProvider defaultAccountProvider;

        public GetTransactionsCommand(IRippleRestClient client, IAccountAndPasswordProvider defaultAccountProvider)
        {
            this.client = client;
            this.defaultAccountProvider = defaultAccountProvider;
        }

        public int Execute(string[] args, TextWriter output, TextWriter errorOutput)
        {
            throw new NotImplementedException("GetTransactions command is not implemented.");
        }
    }
}
