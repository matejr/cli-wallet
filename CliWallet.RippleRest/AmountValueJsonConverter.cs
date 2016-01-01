using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CliWallet.RippleRest
{
    /// <summary>
    /// Serializes/deserializes amount values as strings with invariant culture.
    /// </summary>
    class AmountValueJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(decimal).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return decimal.Parse((string)reader.Value, CultureInfo.InvariantCulture);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var s = Convert.ToString(value, CultureInfo.InvariantCulture);

            writer.WriteValue(s);
        }
    }
}
