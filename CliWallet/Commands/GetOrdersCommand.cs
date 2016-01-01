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
    public class GetOrdersCommand : ICommand
    {
        IRippleRestClient client;
        IAccountAndPasswordProvider defaultAccountProvider;

        public GetOrdersCommand(IRippleRestClient client, IAccountAndPasswordProvider defaultAccountProvider)
        {
            this.client = client;
            this.defaultAccountProvider = defaultAccountProvider;
        }

        public int Execute(string[] args, TextWriter output, TextWriter errorOutput)
        {
            throw new NotImplementedException("GetOrders command is not implemented.");
        }
    }
}
