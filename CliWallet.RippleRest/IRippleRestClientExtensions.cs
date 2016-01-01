using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace CliWallet.RippleRest
{
    /// <summary>
    /// Synchronous versions of methods in <see cref="IRippleRestClient"/>. 
    /// </summary>
    /// <remarks>
    /// <para>Use async versions of methods if you can.</para>
    /// <para>Not all async methods are implemented.</para>
    /// <para>
    /// Async methods are called on thread pool (<see cref="Task.Run(Action)"/> is used).
    /// Instead of <see cref="AggregateException"/> these methods throw inner exception (e.g. <see cref="RippleRestErrorException"/>.
    /// </para>
    /// <para>
    /// This class will be probably removed from this project.
    /// </para>
    /// </remarks>
    [Obsolete("Use async methods instead.")]
    public static class IRippleRestClientExtensions
    {
        [Obsolete("Use async method instead.")]
        public static GetAccountSettingsResponse GetAccountSettings(this IRippleRestClient client, string account)
        {
            try
            {
                return Task.Run<GetAccountSettingsResponse>(async () => await client.GetAccountSettingsAsync(account)).Result;
            }
            catch (AggregateException e)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                return null;
            }
        }

        [Obsolete("Use async method instead.")]
        public static GetAccountBalancesResponse GetAccountBalances(this IRippleRestClient client, string account)
        {
            try
            {
                return Task.Run<GetAccountBalancesResponse>(async () => await client.GetAccountBalancesAsync(account)).Result;
            } catch(AggregateException e)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                return null;
            }
        }

        [Obsolete("Use async method instead.")]
        public static PreparePaymentResponse PreparePayment(this IRippleRestClient client, string address, string destinationAccount, Amount destinationAmount)
        {
            try
            { 
                return Task.Run<PreparePaymentResponse>(async () => await client.PreparePaymentAsync(address, destinationAccount, destinationAmount)).Result;
            }
            catch (AggregateException e)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                return null;
            }
        }

        [Obsolete("Use async method instead.")]
        public static SubmitPaymentAndWaitUntilValidatedResponse SubmitPaymentAndWaitUntilValidated(this IRippleRestClient client, Payment payment, string clientResourceId, string secret)
        {
            try
            {
                return Task.Run<SubmitPaymentAndWaitUntilValidatedResponse>(async () => await client.SubmitPaymentAndWaitUntilValidatedAsync(payment, clientResourceId, secret)).Result;
            }
            catch (AggregateException e)
            {
                ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                return null;
            }
        }

        // TODO: Fix this, add missing methods
    }
}
