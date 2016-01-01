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
    public class GetTrustlinesCommand : ICommand
    {
        IRippleRestClient client;
        IAccountProvider defaultAccountProvider;

        public GetTrustlinesCommand(IRippleRestClient client, IAccountProvider defaultAccountProvider)
        {
            this.client = client;
            this.defaultAccountProvider = defaultAccountProvider;
        }

        public int Execute(string[] args, TextWriter output, TextWriter errorOutput)
        {
            throw new NotImplementedException("GetTrustlines command is not implemented.");
        }
    }
}
