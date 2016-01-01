using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliWallet.RippleRest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CliWallet.RippleRest.Tests.Integration
{
    [TestClass]
    public class RippleRestClientPaymentsIntegrationTests
    {
        string restServer = ConfigurationManager.AppSettings["RippleRestServer"];
        string account1 = "rhYTD7NgJKLV4dWXRiC5QY91VNqDzGhwEc";
        string secret1 = "sn92sbuQQQyvmSVraLCHQHd439iGM";
        string account2 = "rn3fBubaG5t2TUQQXrquQUL8fKuoUMnBL2";
        RippleRestClient client;

        [TestInitialize]
        public void InitializeTest()
        {
            client = new RippleRestClient(restServer);
        }

        [TestMethod]
        public async Task PreparePaymentAsync_SendXrpFromAccount1ToAccount2_PaymentObjectsHaveAccountsAndAmounts()
        {
            var r = await client.PreparePaymentAsync(account1, account2, new Amount(100));

            Assert.IsTrue(r.Success);
            Assert.IsTrue(r.Payments.Any());

            foreach (var payment in r.Payments)
            {
                Assert.AreEqual(account1, payment.SourceAccount);
                Assert.AreEqual(account2, payment.DestinationAccount);
                Assert.AreEqual(new Amount(100), payment.SourceAmount);
                Assert.AreEqual(new Amount(100), payment.DestinationAmount);
            }
        }

        [TestMethod, RequiresSecretKey]
        public async Task SubmitPaymentAndWaitUntilValidated_SendXrpFromAccount1ToAccount2_Succeeds()
        {
            var payment = new Payment() { SourceAccount = account1, DestinationAmount = new Amount(3), DestinationAccount = account2 };

            var guid = Guid.NewGuid().ToString();
             
            var r = await client.SubmitPaymentAndWaitUntilValidatedAsync(payment, guid, secret1);

            Assert.IsTrue(r.Success);
            Assert.AreEqual("validated", r.State);
            Assert.AreEqual("tesSUCCESS", r.Payment.Result);
            Assert.AreEqual(account1, r.Payment.SourceAccount);
            Assert.AreEqual(account2, r.Payment.DestinationAccount);
            Assert.AreEqual(new Amount(3), r.Payment.DestinationBalanceChanges[0]);
            Assert.AreEqual(new Amount(-3.000012M), r.Payment.SourceBalanceChanges[0]);
        }
    }
}