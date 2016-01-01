using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using CliWallet.RippleRest;

namespace CliWallet.RippleRest.Tests.Unit
{
    [TestClass]
    public class RippleRestClientUnitTests
    {
        /// Accounts provided as parameters for methods. 
        /// This is unit test and these accounts don't have to actually exist on the ledger.
        private string account1 = "rHFuLshhwiFBnLQM5oZwDZbaS4gpjLsVMx";
        private string secret1 = "shbJqC3QMQV7dTgf96FbjMbZ7hwG7";
        private string account2 = "rw5DrdVgFR1jNJ2d2oL2aEsNpHwCtF8svp";

        [TestMethod]
        public void SubmitPaymentAndWaitUntilValidatedAsync_AmountValuesAreSerializedAsStrings()
        {
            Payment p = new Payment() {
                SourceAccount = account1, SourceAmount = new Amount(100),
                DestinationAccount = account2, DestinationAmount = new Amount(100)
            };

            var guid = Guid.NewGuid().ToString();

            var requestJson = GetRequestContent(async c => await c.SubmitPaymentAndWaitUntilValidatedAsync(p, guid, secret1));

            AssertHelper.IsStringValue(requestJson, "payment.destination_amount.value");
            AssertHelper.IsStringValue(requestJson, "payment.source_amount.value");
        }

        [TestMethod]
        public void SubmitPaymentAndWaitUntilValidatedAsync_AmountsContainCurrencyAndIssuer()
        {
            Payment p = new Payment()
            {
                SourceAccount = account1,
                SourceAmount = new Amount(100, "EUR", account1),
                DestinationAccount = account2,
                DestinationAmount = new Amount(100, "USD", account2)
            };

            var guid = Guid.NewGuid().ToString();

            var requestJson = GetRequestContent(async c => await c.SubmitPaymentAndWaitUntilValidatedAsync(p, guid, secret1));

            var json = JObject.Parse(requestJson);

            Assert.AreEqual("EUR", json.SelectToken("payment.source_amount.currency").Value<string>());
            Assert.AreEqual(account1, json.SelectToken("payment.source_amount.issuer").Value<string>());
                   
            Assert.AreEqual("USD", json.SelectToken("payment.destination_amount.currency").Value<string>());
            Assert.AreEqual(account2, json.SelectToken("payment.destination_amount.issuer").Value<string>());
        }

        /// <summary>
        /// Gets the string content of the HTTP request created by calling one of the methods of <see cref="RippleRestClient"/>
        /// in <paramref name="methodCall"/>.
        /// </summary>
        /// <param name="methodCall">Action that calls method on <see cref="RippleRestClient"/>.</param>
        /// <returns></returns>
        protected string GetRequestContent(Action<RippleRestClient> methodCall)
        {
            string requestContent = null;

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            // FIXME: return proper response
            responseMessage.Content = new StringContent("{ \"success\": true }");

            var mockHandler = new Mock<MockableHttpClientHandler>();
            mockHandler.CallBase = true;
            mockHandler.Setup(h => h.Send(It.IsAny<HttpRequestMessage>()))
                       .Returns(responseMessage)
                       .Callback<HttpRequestMessage>(async (request) => { requestContent = await request.Content.ReadAsStringAsync(); });

            var client = new RippleRestClient("http://localhost:5990", mockHandler.Object);

            methodCall.Invoke(client);

            mockHandler.Verify(h => h.Send(It.IsAny<HttpRequestMessage>()), Times.Once);

            return requestContent;
        }
    }
}