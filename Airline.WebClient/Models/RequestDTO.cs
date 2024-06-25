using static Airline.WebClient.Utility.SD;

namespace Airline.WebClient.Models.DTOs
{
    public class RequestDTO
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string ApiUrl { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
