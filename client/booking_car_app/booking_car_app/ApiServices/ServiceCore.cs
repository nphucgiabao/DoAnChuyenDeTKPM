using booking_car_app.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace booking_car_app.ApiServices
{
    public abstract class ServiceCore
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServiceCore(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        
        protected async Task<ResponseModel> GetRequest(string url)
        {
            var httpClient = _httpClientFactory.CreateClient("BookingCarAPIClient");

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResponseModel>(content);
            }
            return null;
        }

        protected async Task<ResponseModel> PostRequest<T>(string url, T data)
        {
            try
            {
                //var client = new HttpClient();
                // just checks if we can reach the Discovery document. Not 100% needed but..
                //var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44342");
                //if (disco.IsError)
                //{
                //    return null; // throw 500 error
                //}
                //var apiClientCredentials = new ClientCredentialsTokenRequest
                //{
                //    Address = disco.TokenEndpoint,

                //    ClientId = "bookingClient",
                //    ClientSecret = "secret",

                //    // This is the scope our Protected API requires. 
                //    Scope = "booking_car_api",

                //};     

                // 2. Authenticates and get an access token from Identity Server
                //var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);
                //if (tokenResponse.IsError)
                //{
                //    return null;
                //}

                // Another HttpClient for talking now with our Protected API
                //var apiClient = new HttpClient();

                // 3. Set the access_token in the request Authorization: Bearer <token>
                //client.SetBearerToken(tokenResponse.AccessToken);

                // 4. Send a request to our Protected API
                //var uri = $"https://localhost:44308{url}";
                //var response = await client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(data)));
                //response.EnsureSuccessStatusCode();

                //var content = await response.Content.ReadAsStringAsync();

                var httpClient = _httpClientFactory.CreateClient("BookingCarAPIClient");

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                
                request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ResponseModel>(result);
                }
                return null;
            }
            catch (Exception ex)
            {
                return new ResponseModel { Success = false, Message = ex.ToString() };
            }
            
        }
    }
}
