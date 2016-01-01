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
    public class SignAndSubmitCommand : ICommand
    {
        IRippleRestClient client;
        IAccountProvider defaultAccountAndPasswordProvider;

        public SignAndSubmitCommand(IRippleRestClient client, IAccountProvider defaultAccountAndPasswordProvider)
        {
            this.client = client;
            this.defaultAccountAndPasswordProvider = defaultAccountAndPasswordProvider;
        }

        public int Execute(string[] args, TextWriter output, TextWriter errorOutput)
        {
            throw new NotImplementedException("SignAndSubmit command is not implemented.");
        }
    }
}

