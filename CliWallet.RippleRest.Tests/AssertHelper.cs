using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace CliWallet.RippleRest.Tests
{
    static class AssertHelper
    {
        public static void IsLongString(string value)
        {
            Assert.IsTrue(!string.IsNullOrWhiteSpace(value));
            Assert.IsTrue(value.Length > 3);
        }

        public static void IsStringValue(string json, string path)
        {
            var o = JObject.Parse(json);
            var t = o.SelectToken(path);

            Assert.AreEqual(JTokenType.String, t.Type, $"Path {path} must be string");
        }
    }
}
