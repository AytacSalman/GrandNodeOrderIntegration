using AutoMapper;
using GrandNodeOrderIntegration.Models;
using GrandNodeOrderIntegration.Models.GrandNode;
using GrandNodeOrderIntegration.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GrandNodeOrderIntegration.Services
{
    public class GrandNodeService : IGrandNodeService
    {
        string _token = string.Empty;
        string _baseApiUrl = "http://localhost:8010/";
        private readonly IMapper _mapper;

        public GrandNodeService(IMapper mapper)
        {
            _token = GetToken();
            _mapper = mapper;
        }

        public void IntegrateTrendyolOrders(TrendyolOrderModel trendyolOrderModel)
        {
            if (trendyolOrderModel != null)
            {
                foreach (var item in trendyolOrderModel.Content)
                {
                    var isThereCustomer = GetCustomer(item.CustomerEmail);

                    if (!isThereCustomer)
                    {
                        var customerResponse = CreateCustomer(item);
                    }

                    List<string> trendyolSkuList = item.Lines.Select(w => w.Sku).ToList();
                    bool checkProducts = CheckProducts(trendyolSkuList);

                    // ürün SKU GrandNode da bulunamadı, bir sonraki siparişe geç
                    if (!checkProducts)
                    {
                        Console.Write("Sipariş de tanımlı olmayan ürünler bulunmaktadır.");
                        continue;
                    }

                    CreateOrder(item);
                }
            }
        }

        private bool GetCustomer(string email)
        {
            string url = $"{_baseApiUrl}odata/Customer/{email}";
            string bearerToken = _token;

            using (var client = new HttpClient())
            {
                AddDefaultRequestHeaders(client);
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private HttpWebResponse CreateCustomer(ContentItem contentItem)
        {
            var url = $"{_baseApiUrl}odata/Customer";
            var request = (HttpWebRequest)WebRequest.Create(url);
            AddHttpWebRequestProperties(request);

            var customer = _mapper.Map<CustomerModel>(contentItem);
            var customerJson = JsonSerializer.Serialize(customer, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(customerJson);
            }

            var result = (HttpWebResponse)request.GetResponse();

            if (result.StatusCode == HttpStatusCode.OK)
            {
                CreateShipmentAddress(contentItem);
                CreateInvoiceAddress(contentItem);
            }

            return result;
        }

        private bool CheckProducts(List<string> trendyolSkuList)
        {
            string url = $"{_baseApiUrl}odata/product";

            using (var client = new HttpClient())
            {
                AddDefaultRequestHeaders(client);

                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;

                    JArray grandNodeProducts = JArray.Parse(json);
                    List<string> grandNodeSkuList = new List<string>();

                    foreach (var product in grandNodeProducts)
                    {
                        grandNodeSkuList.Add(product["Sku"].ToString());
                    }

                    return trendyolSkuList.All(item => grandNodeSkuList.Contains(item));
                }
                else
                {
                    return false;
                }
            }
        }

        private void CreateShipmentAddress(ContentItem contentItem)
        {
            string encodedEmail = WebUtility.UrlEncode(contentItem.CustomerEmail);
            var url = $"{_baseApiUrl}odata/Customer/({encodedEmail})/AddAddress";
            var request = (HttpWebRequest)WebRequest.Create(url);
            AddHttpWebRequestProperties(request);

            var customerAddress = _mapper.Map<ShipmentAddressModel>(contentItem);
            var context = new ValidationContext(customerAddress, null, null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(customerAddress, context, results, true);

            if (!isValid)
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
            {
                var customerAddressJson = JsonSerializer.Serialize(customerAddress, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(customerAddressJson);
                }

                var result = (HttpWebResponse)request.GetResponse();
            }
        }

        private void CreateInvoiceAddress(ContentItem contentItem)
        {
            string encodedEmail = WebUtility.UrlEncode(contentItem.CustomerEmail);
            var url = $"{_baseApiUrl}odata/Customer/({encodedEmail})/AddAddress";
            var request = (HttpWebRequest)WebRequest.Create(url);
            AddHttpWebRequestProperties(request);

            var customerAddress = _mapper.Map<InvoiceAddressModel>(contentItem);
            var context = new ValidationContext(customerAddress, null, null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(customerAddress, context, results, true);

            if (!isValid)
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            else
            {
                var customerAddressJson = JsonSerializer.Serialize(customerAddress, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(customerAddressJson);
                }

                var result = (HttpWebResponse)request.GetResponse();
            }
        }

        private void CreateOrder(ContentItem contentItem)
        {
            //... ORDER END-POINT İ YOK
        }

        private void AddDefaultRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void AddHttpWebRequestProperties(HttpWebRequest request)
        {
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers["Authorization"] = $"Bearer {_token}";
        }

        private string GetToken()
        {
            var url = $"{_baseApiUrl}api/token/create";
            var email = "aytacslmn@gmail.com";
            var password = "MTIzNDU2";

            var json = $"{{\"email\": \"{email}\", \"password\": \"{password}\"}}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PostAsync(url, content).GetAwaiter().GetResult();
                var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                return responseContent;
            }
        }
    }
}
