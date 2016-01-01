using System;
using System.IO;
using System.Threading.Tasks;
using CliWallet;
using CliWallet.Commands;
using CliWallet.RippleRest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CliWallet.Tests.Unit
{
    [TestClass]
    public class GetBalanceCommandTests
    {
        string defaultAccount = "rh8PUKX8gqjiFTr2vTpZQuzc362zyp1Ekb";
        Mock<IAccountAndPasswordProvider> accountProviderMock;
        Mock<IRippleRestClient> clientMock;
        StringWriter output;
        StringWriter errorOutput;

        [TestInitialize]
        public void TestInitialize()
        {
            accountProviderMock = new Mock<IAccountAndPasswordProvider>();
            accountProviderMock.SetupGet(p => p.Account).Returns(defaultAccount);

            clientMock = new Mock<IRippleRestClient>(MockBehavior.Strict);

            output = new StringWriter();
            errorOutput = new StringWriter();
        }

        [TestMethod]
        public void GetBalance_NoAccountProvidedInCommandLine_UsesDefaultAccount()
        {
            var response = new GetAccountBalancesResponse()
            {
                Success = true,
                Validated = true,
                Balances = new GetAccountBalancesResponse.Balance[] {
                    new GetAccountBalancesResponse.Balance() {  Value = 3, Currency = "XRP", Counterparty = null }
                }
            };
      
            clientMock.Setup(c => c.GetAccountBalancesAsync(defaultAccount)).Returns(Task.FromResult(response));

            var cmd = new GetBalanceCommand(clientMock.Object, accountProviderMock.Object);

            cmd.Execute(new string[] { }, output, errorOutput);

            Assert.AreEqual("", errorOutput.ToString());
            Assert.AreEqual("", errorOutput.ToString());

            clientMock.Verify(c => c.GetAccountBalancesAsync(defaultAccount), Times.Once);
        }

        [TestMethod]
        public void GetBalance_AccountProvidedInCommandLine_UsesDefaultAccount()
        {
            string account = "rKsphokjVPYm9UqCrarZiNdFTpnTraayKH";

            var response = new GetAccountBalancesResponse()
            {
                Success = true,
                Validated = true, 
                Balances = new GetAccountBalancesResponse.Balance[] {
                    new GetAccountBalancesResponse.Balance() {  Value = 3, Currency = "XRP", Counterparty = null }
                }
            };

            clientMock.Setup(c => c.GetAccountBalancesAsync(account)).Returns(Task.FromResult(response));

            var cmd = new GetBalanceCommand(clientMock.Object, accountProviderMock.Object);

            cmd.Execute(new string[] { account }, output, errorOutput);

            Assert.AreEqual("", errorOutput.ToString());
            Assert.AreEqual("", errorOutput.ToString());

            clientMock.Verify(c => c.GetAccountBalancesAsync(account), Times.Once);
        }

        //FIXME: add removed tests
    }


}