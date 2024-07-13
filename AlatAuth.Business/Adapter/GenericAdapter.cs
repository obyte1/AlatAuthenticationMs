using AlatAuth.Common.Enum;
using AlatAuth.Data.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace AlatAuth.Business.Adapter
{
    public class GenericAdapter
    {
        private readonly IHttpContextAccessor httpContext;
        public ResponseDto responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public GenericAdapter(IHttpClientFactory httpClient, IHttpContextAccessor httpContext)
        {
            responseModel = new ResponseDto();
            this.httpClient = httpClient;
            this.httpContext = httpContext;
        }

        public async Task<T> SendAsync<T>(APIRequestModel apiRequest)
        {
            var scope = httpContext.HttpContext!.RequestServices.CreateScope();
            var _logger = scope.ServiceProvider.GetRequiredService<ILogger<GenericAdapter>>();
            try
            {
                HttpClient client = httpClient.CreateClient("EagleAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }
                if (!string.IsNullOrEmpty(apiRequest.ApiKey))
                    client.DefaultRequestHeaders.Add("apiKey", apiRequest.ApiKey);
                if (!string.IsNullOrEmpty(apiRequest.UserId))
                    client.DefaultRequestHeaders.Add("userId", apiRequest.UserId);

                if (!string.IsNullOrEmpty(apiRequest.xKey))
                    client.DefaultRequestHeaders.Add("x-api-key", apiRequest.xKey);

                HttpResponseMessage apiResponse = null!;
                switch (apiRequest.ApiType)
                {

                    case ApiType.Post:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.Put:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.Delete:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                apiResponse = await client.SendAsync(message);
                string apiContent = await apiResponse.Content.ReadAsStringAsync();
                _logger.Log(LogLevel.Information, apiContent);
                T apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent)!;
                return apiResponseDto;
            }
            catch (Exception ex)
            {

                ResponseDto dto = new ResponseDto
                {
                    Message = "Error",
                    ErrorMessages = new List<string>() { Convert.ToString(ex.InnerException)! },
                    IsSuccess = false,

                };
                _logger.LogError(ex, "Error Occured");
                string res = JsonConvert.SerializeObject(dto);
                T apiResponseDto = JsonConvert.DeserializeObject<T>(res)!;
                return apiResponseDto;
            }
        }
        public async Task<HttpResponseMessage> SendAsync(APIRequestModel apiRequest)
        {
            HttpClient client = httpClient.CreateClient("EagleAPI");
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(apiRequest.Url);
            client.DefaultRequestHeaders.Clear();
            if (apiRequest.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                    Encoding.UTF8, "application/json");
            }
            if (!string.IsNullOrEmpty(apiRequest.Token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer ", apiRequest.Token);
            }
            switch (apiRequest.ApiType)
            {

                case ApiType.Post:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.Put:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiType.Delete:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }
            return await client.SendAsync(message);

        }
        public string UserId()
        {
            // Check if HttpContext is not null
            if (httpContext == null)
            {
                return null!;
            }

            // Retrieve the claims identity from HttpContext
            var claimsIdentity = httpContext.HttpContext!.User.Identity as ClaimsIdentity;

            // Check if claims identity is not null
            if (claimsIdentity == null)
            {
                // No authenticated user, return null or handle the case as needed
                return null!;
            }

            // Find the claim with the user ID
            var userIdClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // If the claim exists, return the user ID
            if (userIdClaim != null)
            {
                return userIdClaim.Value;
            }

            // If user ID claim not found, return null or throw an exception based on your requirements
            // You might want to throw an exception if the user ID claim is expected but not found
            return null!;
        }
        public string UserRole()
        {
            // Check if HttpContext is not null
            if (httpContext == null)
            {
                return null!;
            }

            // Retrieve the claims identity from HttpContext
            var claimsIdentity = httpContext.HttpContext!.User.Identity as ClaimsIdentity;

            // Check if claims identity is not null
            if (claimsIdentity == null)
            {
                // No authenticated user, return null or handle the case as needed
                return null!;
            }

            // Find the claim with the role
            var roleClaim = claimsIdentity.FindFirst(ClaimTypes.Role);

            // If the claim exists, return the role
            if (roleClaim != null)
            {
                return roleClaim.Value;
            }

            // If role claim not found, return null or throw an exception based on your requirements
            // You might want to throw an exception if the role claim is expected but not found
            return null!;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

    }

}
