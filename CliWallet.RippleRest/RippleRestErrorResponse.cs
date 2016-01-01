using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CliWallet.RippleRest
{
    [Serializable]
    public class RippleRestErrorResponse
    {
        public bool Success { get; set; }
        public string ErrorType { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }

        public static bool TryParse(string json, out RippleRestErrorResponse e)
        {
            if (string.IsNullOrEmpty(json))
            {
                e = null;
                return false;
            }

            var settings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Error,
                ContractResolver = new CustomContractResolver()
            };

            try
            {
                e = JsonConvert.DeserializeObject<RippleRestErrorResponse>(json, settings);
            }
            catch (JsonException)
            {
                e = null;
                return false;
            }
            return true;
        }
    }
}
