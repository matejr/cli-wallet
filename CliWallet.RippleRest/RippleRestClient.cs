using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CliWallet.RippleRest
{
    public class RippleRestClient : IRippleRestClient
    {
        protected HttpClient client { get; set; }
        protected JsonSerializerSettings serializerSettings = new JsonSerializerSettings() {
            MissingMemberHandling = MissingMemberHandling.Ignore, ContractResolver = new CustomContractResolver()
        };

        public RippleRestClient(string serverAddress)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(serverAddress);
            client.DefaultRequestHeaders.Add("User-Agent", "Ripple-REST .NET client");
        }

        public RippleRestClient(string serverAddress, HttpMessageHandler messageHandler)
        {
            client = new HttpClient(messageHandler);
            client.BaseAddress = new Uri(serverAddress);
            client.DefaultRequestHeaders.Add("User-Agent", "Ripple-REST .NET client");
        }

        /// <summary>
        /// Randomly generate keys for a potential new Ripple account.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<GenerateWalletResponse> GenerateWalletAsync()
        {
            return await GetJsonAsync<GenerateWalletResponse>($"/v1/wallet/new");
        }

        /// <summary>
        /// Gets the current balances for the given account.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public virtual async Task<GetAccountBalancesResponse> GetAccountBalancesAsync(string account)
        {
            return await GetJsonAsync<GetAccountBalancesResponse>($"/v1/accounts/{account}/balances");
        }

        /// <summary>
        /// Gets the current settings for a given account.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public virtual async Task<GetAccountSettingsResponse> GetAccountSettingsAsync(string account)
        {
            return await GetJsonAsync<GetAccountSettingsResponse>($"/v1/accounts/{account}/settings");
        }

        /// <summary>
        /// Gets the current transaction cost, in XRP, for the rippled server Ripple-REST is connected to.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<GetTransactionFeeResponse> GetTransactionFeeAsync()
        {
            return await GetJsonAsync<GetTransactionFeeResponse>($"/v1/transaction-fee");
        }

        /// <summary>
        /// Gets information about the current status of the Ripple-REST API and the rippled server.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<GetServerStatusResponse> GetServerStatusAsync()
        {
            return await GetJsonAsync<GetServerStatusResponse>($"/v1/server");
        }

        /// <summary>
        /// Generates a universally-unique identifier suitable for use as the Client Resource ID for a payment.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<CreateClientResourceIdResponse> CreateClientResourceIdAsync()
        {
            return await GetJsonAsync<CreateClientResourceIdResponse>($"/v1/uuid");
        }

        /// <summary>
        /// Gets quotes for possible ways to make a particular payment.
        /// </summary>
        /// <param name="sourceAccount">The Ripple address for the account that would send the payment.</param>
        /// <param name="destinationAccount">The Ripple address for the account that would receive the payment.</param> 
        /// <param name="destinationAmount">The amount that the destination account should receive.</param>
        /// <returns></returns>
        public virtual async Task<PreparePaymentResponse> PreparePaymentAsync(string sourceAccount, string destinationAccount, Amount destinationAmount)
        {
            if (sourceAccount == null)
                throw new ArgumentNullException(nameof(sourceAccount));

            if (destinationAccount == null)
                throw new ArgumentNullException(nameof(destinationAccount));

            if (destinationAmount == null)
                throw new ArgumentNullException(nameof(destinationAmount));

            var uri = $"/v1/accounts/{sourceAccount}/payments/paths/{destinationAccount}/{destinationAmount.ToUrlFormattedAmount()}";

            return await GetJsonAsync<PreparePaymentResponse>(uri);
        }

        /// <summary>
        /// Submits a payment and waits until it is validated.
        /// </summary>
        /// <returns></returns> 
        public virtual async Task<SubmitPaymentAndWaitUntilValidatedResponse> SubmitPaymentAndWaitUntilValidatedAsync(Payment payment, string clientResourceId, string secret)
        {
            var obj = new { clientResourceId = clientResourceId, Secret = secret, Payment = payment };

            var json = JsonConvert.SerializeObject(obj, Formatting.Indented, serializerSettings);

            var uri = $"/v1/accounts/{payment.SourceAccount}/payments?validated=true";

            return await PostJsonAsync<SubmitPaymentAndWaitUntilValidatedResponse>(uri, json);
        }

        protected async Task<T> GetJsonAsync<T>(string requestUri)
        {
            HttpResponseMessage response = null;
            string responseContent = null;

            try
            {
                response = await client.GetAsync(requestUri).ConfigureAwait(false);
                responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                RippleRestErrorResponse r;

                if (RippleRestErrorResponse.TryParse(responseContent, out r))
                    throw new RippleRestErrorException(r);
                else
                    throw;
            }

            return JsonConvert.DeserializeObject<T>(responseContent, serializerSettings);
        }

        protected async Task<T> PostJsonAsync<T>(string requestUri, string json)
        {
            HttpResponseMessage response;
            string responseContent = null;

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                response = await client.PostAsync(requestUri, content).ConfigureAwait(false);
                responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                RippleRestErrorResponse r;

                if (RippleRestErrorResponse.TryParse(responseContent, out r))
                    throw new RippleRestErrorException(r);
                else
                    throw;
            }

            return JsonConvert.DeserializeObject<T>(responseContent, serializerSettings);
        }
    }
}

