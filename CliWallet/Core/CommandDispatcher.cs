using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using CliWallet.RippleRest;

namespace CliWallet.Core
{
    class CommandDispatcher
    {
        /// <summary>
        /// Resolves command provided as first argument in <paramref name="args"/> and 
        /// invokes <see cref="ICommand.Execute(string[], TextWriter, TextWriter)"/>.
        /// </summary>
        /// <param name="args">Command line arguments including the first one which represents command.</param>
        /// <param name="container">Unity container with registered ICommand types. Each ICommand type must be registered with a lowercase name
        /// which corresponds to the command name in <paramref name="args"/>.
        /// <param name="output"></param>
        /// <param name="errorOutput"></param>
        public static void DispatchCommand(string[] args, IUnityContainer container, TextWriter output, TextWriter errorOutput)
        {
            if (args.Length == 0)
            {
                errorOutput.WriteLine("No command provided. Try 'wallet help'.");
                return;
            }

            var commandName = args[0].Replace("-", "").ToLowerInvariant();

            if (!container.IsRegistered<ICommand>(commandName))
            {
                errorOutput.WriteLine($"Unknown command '{commandName}'. Try 'wallet help'.");
                return;
            }

            var handler = container.Resolve<ICommand>(commandName);

            try
            {
                // remove first parameter (command name) from the argument list
                var commandArgs = args.Skip(1).ToArray<string>();

                handler.Execute(commandArgs, output, errorOutput);
            }
            // FIXME: this is hack, I don't like this.
            catch (Exception e) when (e is CommandLineArgumentException || e is RippleRestErrorException || e is HttpRequestException ||
                                      e is NotImplementedException)
            {
                errorOutput.WriteLine("Error: " + e.Message);
                return;
            }
        }
    }
}
