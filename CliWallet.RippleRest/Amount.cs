using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace CliWallet.RippleRest
{
    public class Amount : IEquatable<Amount>
    {
        [JsonConverter(typeof(AmountValueJsonConverter))]
        public decimal Value { get; set; }
        public string Currency { get; set; }
        public string Issuer { get; set; }

        [JsonConstructor]
        internal Amount()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Amount"/> class with given amount of XRPs.
        /// </summary>
        public Amount(decimal xrpAmount)
        {
            Value = xrpAmount;
            Currency = "XRP";
            Issuer = "";
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Amount"/> class with given amount of IOUs.
        /// </summary>
        public Amount(decimal amount, string currency, string issuer)
        {
            if (currency == null)
                throw new ArgumentNullException(nameof(currency));

            if (currency == "XRP" && !string.IsNullOrEmpty(issuer))
                throw new ArgumentException("Issuer cannot be set when currency is 'XRP'", nameof(issuer));

            if (issuer == null)
                throw new ArgumentNullException(nameof(issuer));
            
            Value = amount;
            Currency = currency;
            Issuer = issuer??"";
        }

        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        public string ToUrlFormattedAmount()
        {
            if (string.IsNullOrEmpty(Issuer))
                return Value.ToString(CultureInfo.InvariantCulture) + "+" + Currency;
            else
                return Value.ToString(CultureInfo.InvariantCulture) + "+" + Currency + "+" + Issuer;
        }

        public bool Equals(Amount other)
        {
            return Value == other.Value && Currency == other.Currency && Issuer == other.Issuer;
        }

        public override bool Equals(object obj)
        {
            if (obj is Amount)
                return Equals((Amount)obj);

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ (Currency??"").GetHashCode() ^ (Issuer??"").GetHashCode();
        }

        public override string ToString()
        {
            return ToUrlFormattedAmount();
        }

        public static bool operator ==(Amount amount1, Amount amount2)
        {
            if (((object)amount1) == null || ((object)amount2) == null)
                return Object.Equals(amount1, amount2);

            return amount1.Equals(amount2);
        }

        public static bool operator !=(Amount amount1, Amount amount2)
        {
            if (((object)amount1) == null || ((object)amount2) == null)
                return !Object.Equals(amount1, amount2);

            return !(amount1.Equals(amount2));
        }
    }
}
