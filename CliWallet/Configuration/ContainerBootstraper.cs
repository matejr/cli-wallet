using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliWallet.Commands;
using Microsoft.Practices.Unity;
using CliWallet.RippleRest;

namespace CliWallet.Configuration
{
    class ContainerBootstraper
    {
        /// <summary>
        /// Registers all components.
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterComponents(IUnityContainer container)
        {
            var section = (WalletConfigurationSection)ConfigurationManager.GetSection("wallet");

            container.RegisterType<IRippleRestClient, RippleRestClient>(new InjectionConstructor(section.RippleRestServer));
            container.RegisterInstance<IAccountAndPasswordProvider>(section);
            container.RegisterInstance<IAccountAndSecretProvider>(section);

            RegisterCommands(container);
        }

        /// <summary>
        /// Registers all classes that implement <see cref="ICommand"/> interface.
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterCommands(IUnityContainer container)
        {
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies().Where(type => typeof(ICommand).IsAssignableFrom(type)),
                WithMappings.FromAllInterfaces,
                GetCommandNameFromType,
                WithLifetime.ContainerControlled);
        }

        /// <summary>
        /// Gets command-line command name from type name.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Command-line command name.</returns>
        public static string GetCommandNameFromType(Type type)
        {
            return type.Name.Replace("Command", "").ToLowerInvariant();
        }
    }
}

