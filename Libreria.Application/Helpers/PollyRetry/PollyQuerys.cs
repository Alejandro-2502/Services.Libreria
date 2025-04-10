using Libreria.Application.Configuration;
using Polly;

namespace Libreria.Application.Helpers.PollyRetry
{
    public static class PollyQuerys
    {
        public static IAsyncPolicy GetPollyRetry()
        {
            var retryCommand = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(retryCount: ConfigHelper.Pollys!.Querys.Retry, sleepDurationProvider: _ => TimeSpan.FromSeconds(ConfigHelper.Pollys.Querys.FromSeconds));

            return retryCommand;
        }
    }
}
