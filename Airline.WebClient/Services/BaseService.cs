using Airline.WebClient.Models.DTOs;
using Airline.WebClient.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Airline.WebClient.Utilities.SD;

namespace Airline.WebClient.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDTO> SendAsyncCouponAPI(RequestDTO requestDTO)
        {
            try
            {
                HttpClient clientCouponAPI = _httpClientFactory.CreateClient("Airline.Services.CouponAPI");

                HttpRequestMessage messageCouponAPI = new HttpRequestMessage
                {
                    RequestUri = new Uri(clientCouponAPI.BaseAddress, requestDTO.ApiUrl),

                };

                if (requestDTO.Data != null)
                {
                    messageCouponAPI.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");

                }

                switch (requestDTO.ApiType)
                {
                    case ApiType.POST:
                        messageCouponAPI.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        messageCouponAPI.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        messageCouponAPI.Method = HttpMethod.Put;
                        break;
                    default:
                        messageCouponAPI.Method = HttpMethod.Get;
                        break;
                }

                Console.WriteLine($"Base:Request URL: {clientCouponAPI.BaseAddress}");
                Console.WriteLine($"URL:Request URL: {requestDTO.ApiUrl}");
                Console.WriteLine($"Request URL: {messageCouponAPI.RequestUri}");

                HttpResponseMessage apiResponseCouponAPI = await clientCouponAPI.SendAsync(messageCouponAPI);


                if (apiResponseCouponAPI.IsSuccessStatusCode)
                {
                    var apiContentCouponAPI = await apiResponseCouponAPI.Content.ReadAsStringAsync();

                    try
                    {
                        var apiResponseCouponAPIDto = JsonConvert.DeserializeObject<object>(apiContentCouponAPI);
                        return new ResponseDTO { IsSuccess = true, Result = apiContentCouponAPI };
                    }
                    catch (JsonSerializationException)
                    {
                        return new ResponseDTO { IsSuccess = true, Result = JsonConvert.DeserializeObject<object>(apiContentCouponAPI) };
                    }

                }
                else
                {
                    // Log the detailed response for debugging
                    var errorContentCouponAPI = await apiResponseCouponAPI.Content.ReadAsStringAsync();
                    Console.WriteLine($"HTTP Error: {apiResponseCouponAPI.StatusCode}, Content: {errorContentCouponAPI}");

                    switch (apiResponseCouponAPI.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return new ResponseDTO { IsSuccess = false, Message = "Not Found" };
                        case HttpStatusCode.Forbidden:
                            return new ResponseDTO { IsSuccess = false, Message = "Access Denied" };
                        case HttpStatusCode.Unauthorized:
                            return new ResponseDTO { IsSuccess = false, Message = "Unauthorized" };
                        case HttpStatusCode.InternalServerError:
                            return new ResponseDTO { IsSuccess = false, Message = "Internal Server Error" };
                        default:
                            return new ResponseDTO { IsSuccess = false, Message = $"HTTP Error: {apiResponseCouponAPI.StatusCode}" };
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return new ResponseDTO { IsSuccess = false, Message = $"HTTP request error: {ex.Message}" };
            }
            catch (TaskCanceledException ex)
            {
                return new ResponseDTO { IsSuccess = false, Message = $"Request timed out: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { IsSuccess = false, Message = $"Unexpected error: {ex.Message}" };
            }
        }

        public async Task<ResponseDTO> SendAsyncScheduleAPI(RequestDTO requestDTO)
        {
            try
            {
                HttpClient clientScheduleAPI = _httpClientFactory.CreateClient("Airline.Services.ScheduleAPI");

                HttpRequestMessage messageScheduleAPI = new HttpRequestMessage
                {
                    RequestUri = new Uri(clientScheduleAPI.BaseAddress, requestDTO.ApiUrl),
                };

                if (requestDTO.Data != null)
                {
                    messageScheduleAPI.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }

                switch (requestDTO.ApiType)
                {
                    case ApiType.POST:
                        messageScheduleAPI.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        messageScheduleAPI.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        messageScheduleAPI.Method = HttpMethod.Put;
                        break;
                    default:
                        messageScheduleAPI.Method = HttpMethod.Get;
                        break;
                }

                Console.WriteLine($"Base:Request URL: {clientScheduleAPI.BaseAddress}");
                Console.WriteLine($"URL:Request URL: {requestDTO.ApiUrl}");
                Console.WriteLine($"Request URL: {messageScheduleAPI.RequestUri}");

                HttpResponseMessage apiResponseScheduleAPI = await clientScheduleAPI.SendAsync(messageScheduleAPI);

                if (apiResponseScheduleAPI.IsSuccessStatusCode)
                {
                    var apiContentScheduleAPI = await apiResponseScheduleAPI.Content.ReadAsStringAsync();

                    try
                    {
                        var apiResponseScheduleAPIDto = JsonConvert.DeserializeObject<object>(apiContentScheduleAPI);
                        return new ResponseDTO { IsSuccess = true, Result = apiResponseScheduleAPIDto };
                    }
                    catch (JsonSerializationException)
                    {
                        return new ResponseDTO { IsSuccess = true, Result = JsonConvert.DeserializeObject<object>(apiContentScheduleAPI) };
                    }
                }
                else
                {
                    // Log the detailed response for debugging
                    var errorContentScheduleAPI = await apiResponseScheduleAPI.Content.ReadAsStringAsync();
                    Console.WriteLine($"HTTP Error: {apiResponseScheduleAPI.StatusCode}, Content: {errorContentScheduleAPI}");
                    switch (apiResponseScheduleAPI.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return new ResponseDTO { IsSuccess = false, Message = "Not Found" };
                        case HttpStatusCode.Forbidden:
                            return new ResponseDTO { IsSuccess = false, Message = "Access Denied" };
                        case HttpStatusCode.Unauthorized:
                            return new ResponseDTO { IsSuccess = false, Message = "Unauthorized" };
                        case HttpStatusCode.InternalServerError:
                            return new ResponseDTO { IsSuccess = false, Message = "Internal Server Error" };
                        default:
                            return new ResponseDTO { IsSuccess = false, Message = $"HTTP Error: {apiResponseScheduleAPI.StatusCode}" };
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return new ResponseDTO { IsSuccess = false, Message = $"HTTP request error: {ex.Message}" };
            }
            catch (TaskCanceledException ex)
            {
                return new ResponseDTO { IsSuccess = false, Message = $"Request timed out: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new ResponseDTO { IsSuccess = false, Message = $"Unexpected error: {ex.Message}" };
            }
        }
    }
}
