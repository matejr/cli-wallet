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
    public class AmountUnitTests
    {
        [TestMethod]
        public void ToUrlFormattedAmount_10000XRP_WithoutThousandsSeparator()
        {
            Assert.AreEqual("10000+XRP", new Amount(10000).ToUrlFormattedAmount());
        }

        [TestMethod]
        public void ToUrlFormattedAmount_WithDecimals_UsesPeriodAsDecimalSeparator()
        {
            Assert.AreEqual("1.1+XRP", new Amount(1.1M).ToUrlFormattedAmount());
        }

        [TestMethod]
        public void ToUrlFormattedAmount_1BitstampUSD_IncludesUSDAndIssuer()
        {
            Assert.AreEqual("1+USD+rvYAfWj5gh67oV6fW32ZzP3Aw4Eubs59B", new Amount(1M, "USD", "rvYAfWj5gh67oV6fW32ZzP3Aw4Eubs59B").ToUrlFormattedAmount());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_CurrencyNull_ThrowsArgumentNullException()
        {
            new Amount(1M, null, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Ctor_XRPWithIssuer_ThrowsArgumentException()
        {
            new Amount(1M, "XRP", "rvYAfWj5gh67oV6fW32ZzP3Aw4Eubs59B");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_IouWithNullIssuer_ThrowsArgumentNullException()
        {
            new Amount(1M, "XRP", null);
        }

        [TestMethod]
        public void GetHashCode_NullIssuer_ReturnsHashCode()
        {
            new Amount(1M, "XRP", "").GetHashCode();
        }

        [TestMethod]
        public void GetHashCode_TwoDifferentAmounts_GetHashCodeReturnsDifferentValues()
        {
            Assert.AreNotEqual(new Amount(1M, "XRP", "").GetHashCode(), new Amount(1M, "USD", "").GetHashCode());
        }
    }
}