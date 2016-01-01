using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.RippleRest
{
    public class GetAccountBalancesResponse
    {
        public class Balance
        {
            public decimal Value { get; set; }
            public string Currency { get; set; }
            public string Counterparty { get; set; }
        }

        public bool Success { get; set; }
        public int Ledger { get; set; }
        public bool Validated { get; set; }

        // response uses "Counterparty" instead of "Issuer" and 
        // we can't use array of Amount objects.
        public Balance[] Balances;
    }
}
