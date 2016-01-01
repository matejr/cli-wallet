using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.Commands
{
    public class GetVersionCommand : ICommand
    {
        public int Execute(string[] args, TextWriter output, TextWriter errorOutput)
        {
            output.WriteLine(Assembly.GetExecutingAssembly().GetName().Version.ToString());
            return 0;
        }
    }
}
