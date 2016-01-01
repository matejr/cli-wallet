using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliWallet.RippleRest;

namespace CliWallet.Commands
{
    public class SendCommand : ICommand
    {
        IRippleRestClient client;
        IAccountAndSecretProvider defaultAccountAndSecretProvider;

        class SendCommandArgs
        {
            public string FromAccount { get; set; }
            public string Secret { get; set; }
            public string ToAccount { get; set; }
            public Amount Amount { get; set; }
        }

        public SendCommand(IRippleRestClient client, IAccountAndSecretProvider defaultAccountAndSecretProvider)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            this.client = client;
            this.defaultAccountAndSecretProvider = defaultAccountAndSecretProvider;
        }

        public int Execute(string[] args, TextWriter output, TextWriter errorOutput)
        {
            var parsedArgs = ParseCommandLineArguments(args);

            output.WriteLine("Preparing payment...");

            var r = client.PreparePayment(parsedArgs.FromAccount, parsedArgs.ToAccount, parsedArgs.Amount);

            var payment = r.Payments[0];

            output.WriteLine("Submitting payment...");

            var r2 = client.SubmitPaymentAndWaitUntilValidated(payment, Guid.NewGuid().ToString(), parsedArgs.Secret);

            output.WriteLine($"Done, Ripple-REST returned '{r2.Payment.Result}'");

            return 0;
        }

        SendCommandArgs ParseCommandLineArguments(string[] args)
        {
            var a = new SendCommandArgs()
            {
                FromAccount = defaultAccountAndSecretProvider.Account,
                Secret = defaultAccountAndSecretProvider.Secret,
            };

            if (args.Length > 2)
                throw new CommandLineArgumentException("Too many arguments. Please provide amount (e.g. 100+XRP) and destination account.");

            if(args.Length < 2)
                throw new CommandLineArgumentException("Not enough arguments. Please provide amount (e.g. 100+XRP) and destination account.");

            a.Amount = ParseAmount(args[0]);

            a.ToAccount = args[1];

            if (!a.ToAccount.StartsWith("r"))
                throw new CommandLineArgumentException("Account must start with 'r'");

            return a;
        }

        public Amount ParseAmount(string value)
        {
            var s = value.Split('+');

            if (s.Length != 2 && s.Length != 3)
                throw new CommandLineArgumentException($"Invalid amount format '{value}'");

            if (s.Length == 2)
            {
                if (s[1] != "XRP")
                    throw new CommandLineArgumentException("Only XRP can be without issuer.");

                decimal v;
                if (!decimal.TryParse(s[0], NumberStyles.None, CultureInfo.InvariantCulture, out v))
                    throw new CommandLineArgumentException($"Can't parse decimal '{value}'.");

                return new Amount(v);
            }

            throw new NotImplementedException("Parsing of IOU values is not yet implemented.");
        }
    }
}
