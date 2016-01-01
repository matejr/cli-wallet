using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CliWallet.RippleRest;

namespace CliWallet.RippleRest.Tests.Unit
{
    [TestClass]
    public class RippleRestErrorResponseUnitTests
    {
        [TestMethod]
        public void TryParse_CannotGetText_ReturnsFalse()
        {
            RippleRestErrorResponse r;

            Assert.IsFalse(RippleRestErrorResponse.TryParse("Cannot GET /v1/server2", out r));
            Assert.IsNull(r);
        }

        [TestMethod]
        public void TryParse_EmptyJson_ReturnsFalse()
        {
            RippleRestErrorResponse r;

            Assert.IsFalse(RippleRestErrorResponse.TryParse("", out r));
            Assert.IsNull(r);
        }

        [TestMethod]
        public void TryParse_NonErrorJson_ReturnsFalse()
        {
            RippleRestErrorResponse r;

            string json = "{ \"value\": \"hello\" }";

            Assert.IsFalse(RippleRestErrorResponse.TryParse(json, out r));
            Assert.IsNull(r);
        }

        [TestMethod]
        public void TryParse_ErrorJson_ReturnsTrueAndDeserializesAllProperties()
        {
            RippleRestErrorResponse r;

            string json = @"{
                        ""success"": false,
                        ""error_type"": ""invalid_request"",
                        ""message"": ""Parameter is not a valid Ripple address: account"",
                        ""error"": ""restINVALID_PARAMETER""
                        }";

            Assert.IsTrue(RippleRestErrorResponse.TryParse(json, out r));
            Assert.IsNotNull(r);

            Assert.AreEqual(false, r.Success);
            Assert.AreEqual("invalid_request", r.ErrorType);
            Assert.AreEqual("Parameter is not a valid Ripple address: account", r.Message);
            Assert.AreEqual("restINVALID_PARAMETER", r.Error);
        }
    }
}