using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace CliWallet.RippleRest
{

    /// <summary>
    /// Resolves member mappings for a type, pascal casing property names.
    /// </summary>
    class CustomContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomContractResolver"/> class.
        /// </summary>
        public CustomContractResolver()
        {
        }

        /// <summary>
        /// Resolves the name of the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The property name with underscores between words and in lower case.</returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            var sb = new StringBuilder();
            var firstChar = true;

            foreach(var c in propertyName)
            {
                if (Char.IsUpper(c))
                {
                    if(!firstChar)
                        sb.Append('_');

                    sb.Append(Char.ToLowerInvariant(c));
                } else
                {
                    sb.Append(c);
                }

                firstChar = false;
            }

            return sb.ToString();
        }
    }
}
