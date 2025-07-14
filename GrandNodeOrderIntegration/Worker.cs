using GrandNodeOrderIntegration.Services.Interfaces;

namespace GrandNodeOrderIntegration
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITrendyolService _trendyolService;
        private readonly IGrandNodeService _grandNodeService;

        public Worker(ILogger<Worker> logger,
                     ITrendyolService trendyolService,
                     IGrandNodeService grandNodeService)
        {
            _logger = logger;
            _trendyolService = trendyolService;
            _grandNodeService = grandNodeService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var trendyolOrders = _trendyolService.GetTrendyolOrders();
            _grandNodeService.IntegrateTrendyolOrders(trendyolOrders);
        }
    }
}
