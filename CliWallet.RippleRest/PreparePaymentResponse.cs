using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.RippleRest
{

    public class PreparePaymentResponse
    {
        public bool Success { get; set; }
        public Payment[] Payments;
    }
}
