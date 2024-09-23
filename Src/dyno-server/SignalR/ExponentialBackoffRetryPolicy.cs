using Microsoft.AspNetCore.SignalR.Client;

namespace dyno_server.SignalR
{
    public class ExponentialBackoffRetryPolicy : IRetryPolicy
    {
        private readonly TimeSpan _initialDelay;
        private readonly TimeSpan _maxDelay;
        private readonly int _maxAttempts;

        public ExponentialBackoffRetryPolicy(TimeSpan initialDelay, TimeSpan maxDelay, int maxAttempts)
        {
            _initialDelay = initialDelay;
            _maxDelay = maxDelay;
            _maxAttempts = maxAttempts;
        }

        public TimeSpan? NextRetryDelay(RetryContext retryContext)
        {
            // Stop retrying if the maximum number of attempts has been reached
            if (retryContext.PreviousRetryCount >= _maxAttempts)
            {
                return null;  // Stop retrying
            }

            // Calculate the delay as an exponential backoff
            var delay = TimeSpan.FromMilliseconds(_initialDelay.TotalMilliseconds * Math.Pow(2, retryContext.PreviousRetryCount));

            // Cap the delay at the maximum delay
            return delay > _maxDelay ? _maxDelay : delay;
        }
    }
}
