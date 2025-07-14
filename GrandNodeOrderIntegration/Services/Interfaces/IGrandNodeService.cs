using GrandNodeOrderIntegration.Models;

namespace GrandNodeOrderIntegration.Services.Interfaces
{
    public interface IGrandNodeService
    {
        public void IntegrateTrendyolOrders(TrendyolOrderModel trendyolOrderModel);
    }
}
