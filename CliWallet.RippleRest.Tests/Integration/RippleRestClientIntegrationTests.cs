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
    public class RippleRestClientIntegrationTests
    {
        //string _restServer = "https://api.altnet.rippletest.net:5990";
        string restServer = ConfigurationManager.AppSettings["RippleRestServer"];

        string account1 = "rUpcwBefT35qMSBmas46YB5B8spphguP84";
        string invalidAccount = "rINVALID";
        RippleRestClient client;

        [TestInitialize]
        public void InitializeTest()
        {
            client = new RippleRestClient(restServer);
        }

        [TestMethod]
        public async Task GenerateWalletAsync_ReturnsAddressAndSecret()
        {
            var r = await client.GenerateWalletAsync();

            Assert.IsTrue(r.Success);
            AssertHelper.IsLongString(r.Wallet.Address);
            AssertHelper.IsLongString(r.Wallet.Secret);
            Assert.IsTrue(r.Wallet.Address.StartsWith("r"));
        }

        [TestMethod]
        public async Task GetAccountSettingsAsync_ValidAccount_ContainsAccountAndTransactionSequence()
        {
            var r = await client.GetAccountSettingsAsync(account1);

            Assert.IsTrue(r.Success);
            Assert.AreEqual(account1, r.Settings.Account);
            Assert.IsTrue(r.Settings.TransactionSequence > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(RippleRestErrorException))]
        public async Task GetAccountSettingsAsync_InvalidAccount_RippleRestException()
        {
            var r = await client.GetAccountSettingsAsync(invalidAccount);
        }

        [TestMethod]
        public async Task GetAccountBalancesAsync_ValidAccount_ReturnsXRPBalance()
        {
            var r = await client.GetAccountBalancesAsync(account1);

            Assert.IsTrue(r.Success);
            Assert.IsTrue(r.Balances.Length > 0);

            // response should contain XRP balance 
            var xrpBalance = r.Balances.FirstOrDefault(b => b.Currency == "XRP");

            Assert.IsNotNull(xrpBalance, "XRP balance is missing");
            Assert.IsTrue(String.IsNullOrWhiteSpace(xrpBalance.Counterparty));
            Assert.IsTrue(xrpBalance.Value > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(RippleRestErrorException))]
        public async Task GetAccountBalancesAsync_InvalidAccount_RippleRestException()
        {
            var r = await client.GetAccountBalancesAsync(invalidAccount);
        }

        [TestMethod]
        public async Task GetServerStatusAsyncTest()
        {
            var r = await client.GetServerStatusAsync();

            Assert.IsTrue(r.Success);
            AssertHelper.IsLongString(r.ApiDocumentationUrl);
            AssertHelper.IsLongString(r.RippledServerUrl);
            AssertHelper.IsLongString(r.RippledServerStatus.BuildVersion);
            AssertHelper.IsLongString(r.RippledServerStatus.HostId);
            AssertHelper.IsLongString(r.RippledServerStatus.ServerState);
            AssertHelper.IsLongString(r.RippledServerStatus.PubKeyNode);
            Assert.IsTrue(r.RippledServerStatus.ValidatedLedger.Seq > 0);
        }

        [TestMethod]
        public async Task GetTransactionFeeAsyncTest()
        {
            var r = await client.GetTransactionFeeAsync();

            Assert.IsTrue(r.Success);
            Assert.IsTrue(r.Fee > 0);
        }

        [TestMethod]
        public async Task CreateClientResourceIdAsyncTest()
        {
            var r = await client.CreateClientResourceIdAsync();

            Assert.IsTrue(r.Success);
            AssertHelper.IsLongString(r.Uuid);
        }
    }
}