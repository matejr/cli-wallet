using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.RippleRest
{
    public class Payment
    {
        public string SourceAccount { get; set; }
        public string SourceTag { get; set; }
        public Amount SourceAmount { get; set; }
        public decimal SourceSlippage { get; set; }
        public string DestinationAccount { get; set; }
        public string DestinationTag { get; set; }
        public Amount DestinationAmount { get; set; }
        public string InvoiceId { get; set; }
        public string Paths { get; set; } = "[]";
        public bool PartialPayment { get; set; }
        public bool NoDirectRipple { get; set; }
    }
}
