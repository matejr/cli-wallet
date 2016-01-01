using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.RippleRest
{

    public class CreateClientResourceIdResponse
    {
        public bool Success { get; set; }

        public string Uuid { get; set; }
    }

}
