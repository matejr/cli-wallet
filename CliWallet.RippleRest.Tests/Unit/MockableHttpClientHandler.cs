using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CliWallet.RippleRest.Tests.Unit
{
    /// <summary>
    /// Exposes protected method <see cref="HttpClientHandler.SendAsync(HttpRequestMessage, CancellationToken)"/> as a public method 
    /// <see cref="MockableHttpClientHandler.Send(HttpRequestMessage)"/>.
    /// </summary>
    public class MockableHttpClientHandler : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Send(request));
        }

        public virtual HttpResponseMessage Send(HttpRequestMessage request) => null;
    }
}
