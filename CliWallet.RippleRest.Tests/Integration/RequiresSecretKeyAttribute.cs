using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CliWallet.RippleRest.Tests.Integration
{
    /// <summary>
    /// Indicates that test requires secret key and therefore can modify the ledger (changes account settings, creates transactions, orders, etc.)
    /// </summary>
    class RequiresSecretKeyAttribute : TestCategoryBaseAttribute
    {
        public override IList<string> TestCategories
        {
            get
            {
                return new List<string> { "RequiresSecretKeyAttribute" };
            }
        }
    }
}
