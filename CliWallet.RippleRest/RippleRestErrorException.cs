using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CliWallet.RippleRest
{
    /// <summary>
    /// An exception that allows for a given <see cref="RippleRestErrorResponse"/> to be returned to the client.
    /// This exception is thrown only when the error message can be properly deserialized from the HTTP response.
    /// </summary>
    public class RippleRestErrorException : Exception
    {
        public RippleRestErrorResponse ErrorResponse { get; set; }

        public RippleRestErrorException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RippleRestErrorException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public RippleRestErrorException(RippleRestErrorResponse response) : base(response.Message)
        {
            ErrorResponse = response;
        }
    }
}
