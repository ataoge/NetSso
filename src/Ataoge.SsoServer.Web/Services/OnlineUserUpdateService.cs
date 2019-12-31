using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ataoge.SsoServer.Web.Services
{
    public class OnlineUserUpdateService : IHostedService, IDisposable
    {
        public OnlineUserUpdateService(IOnlineUserService onlineUserSerive, ILogger<OnlineUserUpdateService> logger)
        {
            _onlineUserSerive = onlineUserSerive;
            _logger = logger;
        }

        private Task _executingTask;

        private int _checkUpdateTime = 1000 * 60;
        private readonly CancellationTokenSource _stoppingCts = 
                                                   new CancellationTokenSource();

        protected async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"OnlineUser ManagerService is starting.");

            stoppingToken.Register(() => 
                    _logger.LogDebug($" OnlineUser Updated background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"OnlineUser Updated task doing background work.");

                // This eShopOnContainers method is querying a database table 
                // and publishing events into the Event Bus (RabbitMS / ServiceBus)
                _logger.LogInformation("OnlineUser Updateing");
                if (_onlineUserSerive != null)
                {
                    try
                    {
                     _onlineUserSerive.Update(10);
                    }
                    catch(Exception e){
                         _logger.LogError(e.Message);
                    }
                }

                await Task.Delay(_checkUpdateTime, stoppingToken);
            }

            _logger.LogDebug($"GracePeriod background task is stopping.");
        }

        private readonly IOnlineUserService _onlineUserSerive;
        private readonly ILogger<OnlineUserUpdateService> _logger;
        public void Dispose()
        {
            _stoppingCts.Cancel();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Store the task we're executing
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            // If the task is completed then return it, 
            // this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            // Otherwise it's running
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Signal cancellation to the executing method
                _stoppingCts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                                                            cancellationToken));
            }

        }
    }
}