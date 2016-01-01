using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace CliWallet.Commands
{
    public class HelpCommand : ICommand
    {
        IUnityContainer container;

        public HelpCommand(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            this.container = container;
        }

        public int Execute(string[] args, TextWriter output, TextWriter errorOutput)
        {
            output.WriteLine("Known commands (some are not implemented):");

            var commands = container.Registrations
                .Where(r => r.RegisteredType == typeof(ICommand))
                .Select(r => r.Name)
                .OrderBy(r => r)
                .ToList<string>();

            foreach (var c in commands)
                output.WriteLine(c);

            return 0;
        }
    }
}
