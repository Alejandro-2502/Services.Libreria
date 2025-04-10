using Libreria.Application.Configuration;
using Polly;

namespace Libreria.Application.Helpers.PollyRetry
{
    public static class PollyCommand
    {
        public static IAsyncPolicy GetPollyRetry()
        {
            var retryCommand = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(retryCount: ConfigHelper.Pollys!.Commands.Retry, sleepDurationProvider: _ => TimeSpan.FromSeconds(ConfigHelper.Pollys.Commands.FromSeconds));

            return retryCommand;
        }
    }
}
