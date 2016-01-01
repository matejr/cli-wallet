using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.RippleRest
{
    public interface IRippleRestClient
    {
        /// <summary>
        /// Randomly generate keys for a potential new Ripple account.
        /// </summary>
        /// <returns></returns>
        Task<GenerateWalletResponse> GenerateWalletAsync();

        /// <summary>
        /// Gets the current balances for the given account.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<GetAccountBalancesResponse> GetAccountBalancesAsync(string account);

        /// <summary>
        /// Gets the current settings for a given account.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<GetAccountSettingsResponse> GetAccountSettingsAsync(string account);

        /// <summary>
        /// Gets the current transaction cost, in XRP, for the rippled server Ripple-REST is connected to.
        /// </summary>
        /// <returns></returns>
        Task<GetTransactionFeeResponse> GetTransactionFeeAsync();

        /// <summary>
        /// Gets information about the current status of the Ripple-REST API and the rippled server.
        /// </summary>
        /// <returns></returns>
        Task<GetServerStatusResponse> GetServerStatusAsync();

        /// <summary>
        /// Creates a universally-unique identifier suitable for use as the Client Resource ID for a payment.
        /// </summary>
        /// <returns></returns>
        Task<CreateClientResourceIdResponse> CreateClientResourceIdAsync();

        /// <summary>
        /// Gets quotes for possible ways to make a particular payment.
        /// </summary>
        /// <param name="address">The Ripple address for the account that would send the payment.</param>
        /// <param name="destinationAccount">The Ripple address for the account that would receive the payment.</param> 
        /// <param name="destinationAmount">The amount that the destination account should receive.</param>
        /// <returns></returns>
        Task<PreparePaymentResponse> PreparePaymentAsync(string address, string destinationAccount, Amount destinationAmount);

        /// <summary>
        /// Submits a payment and waits until it is validated.
        /// </summary>
        /// <returns></returns>
        Task<SubmitPaymentAndWaitUntilValidatedResponse> SubmitPaymentAndWaitUntilValidatedAsync(Payment payment, string clientResourceId, string secret);
    }
}
