using System;
using CliWallet.Configuration;
using CliWallet.Core;
using Microsoft.Practices.Unity;

namespace CliWallet
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var container = new UnityContainer())
            {
                ContainerBootstraper.RegisterComponents(container);
                CommandDispatcher.DispatchCommand(args, container, Console.Out, Console.Error);
            }
        }
    }
}
