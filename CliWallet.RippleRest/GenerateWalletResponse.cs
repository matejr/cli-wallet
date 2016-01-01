using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.RippleRest
{
    public class GenerateWalletResponse
    {
        public class GeneratedWallet
        {
            public string Address { get; set; }
            public string Secret { get; set; }
        }

        public bool Success { get; set; }
        public GeneratedWallet Wallet { get; set; }
    }
}
