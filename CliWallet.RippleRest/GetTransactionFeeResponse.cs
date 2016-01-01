using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.RippleRest
{

    public class GetTransactionFeeResponse
    {
        public bool Success { get; set; }
        public decimal Fee { get; set; }
    }

}
