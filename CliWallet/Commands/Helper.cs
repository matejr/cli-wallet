using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.Commands
{
    class Helper
    {
        /// <summary>
        /// Parses account from command line arguments. If no account is provided, returns default account. 
        /// Throws <see cref="CommandLineArgumentException"/> if there are other options besides account.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="defaultAccountProvider"></param>
        /// <returns></returns>
        public static string ParseAccountOrDefault(string[] args, IAccountProvider defaultAccountProvider)
        {
            if (args.Length == 0)
                return defaultAccountProvider.Account;

            if (args.Length > 1)
                throw new CommandLineArgumentException("Too many parameters. Only one parameter expected.");

            if (!args[0].StartsWith("r"))
                throw new CommandLineArgumentException($"Invalid parameter. Expecting Ripple account instead of {args[0]}");

            return args[0];
        }
    }
}
