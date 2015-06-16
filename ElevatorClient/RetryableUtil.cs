using System;
using System.Threading;

namespace ElevatorClient
{
    class RetryableUtil
    {
        const int DefaultRetries = 5;
        const int DefaultDelay = 200;

        public static T InvokeWithRetry<T>(Func<T> retryable)
        {
            return InvokeWithRetry(retryable, DefaultRetries, DefaultDelay);
        }

        public static T InvokeWithRetry<T>(Func<T> retryable, int retries, int delay)
        {
            var attempt = 0;

            T result = default(T);

            while (++attempt <= retries)
            {
                try
                {
                    result = retryable();
                }
                catch (Exception e)
                {
                    if (attempt == retries || !IsRetryable(e))
                    {
                        throw;
                    }

                    Thread.Sleep(delay);
                }
            }

            return result;
        }

        private static bool IsRetryable(Exception e)
        {
            // TODO: be smarter about retries here -- if it's a fault in the
            //       server itself, retrying won't make any difference.
            return true;
        }
    }
}
