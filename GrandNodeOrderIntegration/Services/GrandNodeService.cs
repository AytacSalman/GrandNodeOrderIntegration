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

        /// <summary>
        /// Processes and integrates the given Trendyol order model into the internal system.
        /// This method transfers order data received from Trendyol into the local order management system.
        /// </summary>
        /// <param name="trendyolOrderModel">The model containing order data retrieved from Trendyol.</param>
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

        /// <summary>
        /// Retrieves customer information based on the provided email address.
        /// Returns true if the customer exists; otherwise, false.
        /// </summary>
        /// <param name="email">The email address of the customer to look up.</param>
        /// <returns>True if the customer is found; otherwise, false.</returns>
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

        /// <summary>
        /// Sends a request to create a new customer based on the provided content item.
        /// Returns the HTTP response from the external service or API.
        /// </summary>
        /// <param name="contentItem">The content item containing customer data to be sent in the request.</param>
        /// <returns>The HTTP response received after attempting to create the customer.</returns>
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

        /// <summary>
        /// Checks whether the products with the given Trendyol SKUs exist or meet specific criteria in the system.
        /// Returns true if all products pass the check; otherwise, false.
        /// </summary>
        /// <param name="trendyolSkuList">A list of Trendyol product SKUs to be verified.</param>
        /// <returns>True if the products are valid or exist; otherwise, false.</returns>
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

        /// <summary>
        /// Creates and stores a shipment address using the data provided in the given content item.
        /// This method extracts address details and prepares them for use in the shipping process.
        /// </summary>
        /// <param name="contentItem">The content item containing shipment address information.</param>
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

        /// <summary>
        /// Creates and stores an invoice address based on the information provided in the given content item.
        /// This method prepares the billing address details for use in the invoicing process.
        /// </summary>
        /// <param name="contentItem">The content item containing invoice address information.</param>
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

        /// <summary>
        /// Creates a new order using the data provided in the specified content item.
        /// This method extracts order details and initiates the order creation process within the system.
        /// </summary>
        /// <param name="contentItem">The content item containing order information.</param>
        private void CreateOrder(ContentItem contentItem)
        {
            //... ORDER END-POINT İ YOK
        }

        /// <summary>
        /// Adds default HTTP request headers to the specified <see cref="HttpClient"/> instance.
        /// This typically includes headers such as authorization, content type, or custom headers required by the API.
        /// </summary>
        /// <param name="client">The <see cref="HttpClient"/> instance to which the headers will be added.</param>
        private void AddDefaultRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Configures and adds default properties to the specified <see cref="HttpWebRequest"/> instance.
        /// This may include setting headers, timeouts, content type, or other request-specific settings.
        /// </summary>
        /// <param name="request">The <see cref="HttpWebRequest"/> object to configure.</param>
        private void AddHttpWebRequestProperties(HttpWebRequest request)
        {
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers["Authorization"] = $"Bearer {_token}";
        }

        /// <summary>
        /// Retrieves an authentication token used for authorized API requests.
        /// </summary>
        /// <returns>A string representing the authentication token.</returns>
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
