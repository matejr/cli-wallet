using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.Configuration
{

    public class WalletConfigurationSection : ConfigurationSection, IAccountAndSecretProvider
    {

        [ConfigurationProperty("rippleRestServer", DefaultValue = "https://api.ripple.com", IsRequired = false)]
        public string RippleRestServer
        {
            get
            {
                return (string)this["rippleRestServer"];
            }
            set
            {
                this["rippleRestServer"] = value;
            }
        }

        [ConfigurationProperty("account", IsRequired = true)]
        public string Account
        {
            get
            {
                return (string)this["account"];
            }
            set
            {
                this["account"] = value;
            }
        }

        [ConfigurationProperty("secret", IsRequired = true)]
        public string Secret
        {
            get
            {
                return (string)this["secret"];
            }
            set
            {
                this["secret"] = value;
            }
        }
    }
}
