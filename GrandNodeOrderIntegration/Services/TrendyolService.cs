using GrandNodeOrderIntegration.Models;
using GrandNodeOrderIntegration.Services.Interfaces;
using Newtonsoft.Json;

namespace GrandNodeOrderIntegration.Services
{
    public class TrendyolService : ITrendyolService
    {
        /// <summary>
        /// Retrieves the latest Trendyol orders from the external service or API.
        /// </summary>
        /// <returns>A <see cref="TrendyolOrderModel"/> containing the retrieved order data.</returns>
        public TrendyolOrderModel GetTrendyolOrders()
        {
            TrendyolOrderModel result = new();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "trednyol-order-example.json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                    {
                        NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
                    }
                };

                TrendyolOrderModel trendyolOrderModel = new();
                trendyolOrderModel = JsonConvert.DeserializeObject<TrendyolOrderModel>(json, settings);

                result = trendyolOrderModel;
            }

            return result;
        }
    }
}
