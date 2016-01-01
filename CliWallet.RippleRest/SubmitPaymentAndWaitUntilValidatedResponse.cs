using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.RippleRest
{
    public class SubmitPaymentAndWaitUntilValidatedResponse
    {
        public class ValidatedPayment
        {
            public string SourceAccount { get; set; }
            public string SourceTag { get; set; }
            public Amount SourceAmount { get; set; }
            public string SourceSlippage { get; set; }
            public string DestinationAccount { get; set; }
            public string DestinationTag { get; set; }
            public Amount DestinationAmount { get; set; }
            public string InvoiceId { get; set; }
            public string Paths { get; set; }
            public bool NoDirectRipple { get; set; }
            public bool PartialPayment { get; set; }
            public string Direction { get; set; }
            public DateTime Timestamp { get; set; }
            public decimal Fee { get; set; }
            public string Result { get; set; }
            public Amount[] BalanceChanges { get; set; }
            public Amount[] SourceBalanceChanges { get; set; }
            public Amount[] DestinationBalanceChanges { get; set; }
            public object[] OrderChanges { get; set; }
        }

        public ValidatedPayment Payment { get; set; }
        public string ClientResourceId { get; set; }
        public string Hash { get; set; }
        public string Ledger { get; set; }
        public string State { get; set; }
        public bool Success { get; set; }
    }
}
