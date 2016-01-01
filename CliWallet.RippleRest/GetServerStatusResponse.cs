using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CliWallet.RippleRest
{

    public class GetServerStatusResponse
    {
        public bool Success { get; set; }
        public string ApiDocumentationUrl { get; set; }
        public string RippledServerUrl { get; set; }
        public RippledServerStatus RippledServerStatus { get; set; }
    }

    public class RippledServerStatus
    {
        public string BuildVersion { get; set; }
        public string CompleteLedgers { get; set; }

        [JsonProperty(PropertyName = "hostid")]
        public string HostId { get; set; }

        public decimal IOLatencyMs { get; set; }
        public LastClose LastClose { get; set; }
        public decimal LoadFactor { get; set; }
        public int Peers { get; set; }

        [JsonProperty(PropertyName = "pubkey_node")]
        public string PubKeyNode { get; set; }
        public string ServerState { get; set; }
        public ValidatedLedger ValidatedLedger { get; set; }
        public int ValidationQuorum { get; set; }
    }

    public class LastClose
    {
        public decimal ConvergeTimeS { get; set; }
        public int Proposers { get; set; }
    }

    public class ValidatedLedger
    {
        public int Age { get; set; }
        public decimal BaseFeeXrp { get; set; }
        public string Hash { get; set; }
        public decimal ReserveBaseXrp { get; set; }
        public decimal ReserveIncXrp { get; set; }
        public int Seq { get; set; }
    }

}
