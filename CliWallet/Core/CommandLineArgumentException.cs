using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet
{
    class CommandLineArgumentException : Exception
    {
        public CommandLineArgumentException(string message) : base(message)
        { }
    }
}
