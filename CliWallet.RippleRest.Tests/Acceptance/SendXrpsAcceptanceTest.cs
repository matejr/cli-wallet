using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CliWallet.RippleRest;
using System.Configuration;

namespace CliWallet.RippleRest.Tests.Acceptance
{
    /// <summary>
    /// Sends XRPs from one account to another.
    /// </summary>
    /// <remarks>
    /// User story: I would like to use this library to send XRPs from one account to another and get the final XRP balances of both accounts.
    /// </remarks>
    [TestClass]
    public class SendXrpAcceptanceTest
    {
        string restServer = ConfigurationManager.AppSettings["RippleRestServer"];
        string account1 = "rUzJo3vQA2cLgrJpK5vR5PKDchrB51QJA3";
        string secret1 = "ssrVhs8ih36DbafeXUTKNSaDKFUym";
        string account2 = "rpPcjRHgd18AnNZgTSokB5xPpUTJwaMTt4";

        /// <summary>
        /// Amount of XRPs that will be transfered from one account to another.
        /// </summary>
        decimal amountToSend = 8M;

        /// <summary>
        /// Fee that has to be included in calculation of the final balance.
        /// </summary>
        decimal fee = 0.000012M;

        RippleRestClient _client;

        [TestInitialize]
        public void InitializeTest()
        {
            _client = new RippleRestClient(restServer);
        }

        [TestMethod]
        public async Task SendXrpFromAccount1ToAccount2()
        {
            // get initial XRP balance for account1 and account2
            var r1 = await _client.GetAccountBalancesAsync(account1);
            decimal initialXrpBalanceAccount1 = r1.Balances.Single(b => b.Currency == "XRP").Value;
            r1 = await _client.GetAccountBalancesAsync(account2);
            decimal initialXrpBalanceAccount2 = r1.Balances.Single(b => b.Currency == "XRP").Value;

            // prepare payment
            var r2 = await _client.PreparePaymentAsync(account1, account2, new Amount(amountToSend));
            var payment = r2.Payments[0];

            // generate client resource id
            var rid = await _client.CreateClientResourceIdAsync();
            var clientResourceId = rid.Uuid;

            // submit payment 
            var r3 = await _client.SubmitPaymentAndWaitUntilValidatedAsync(payment, clientResourceId, secret1);

            // get final balance for both accounts
            r1 = await _client.GetAccountBalancesAsync(account1);
            decimal finalXrpBalanceAccount1 = r1.Balances.Single(b => b.Currency == "XRP").Value;
            r1 = await _client.GetAccountBalancesAsync(account2);
            decimal finalXrpBalanceAccount2 = r1.Balances.Single(b => b.Currency == "XRP").Value;

            // verify final account balances
            Assert.AreEqual(finalXrpBalanceAccount1, initialXrpBalanceAccount1 - amountToSend - fee, "finalXrpBalanceAccount1 is wrong");
            Assert.AreEqual(finalXrpBalanceAccount2, initialXrpBalanceAccount2 + amountToSend, "finalXrpBalanceAccount2 is wrong");
        }
    }
}