using Airline.WebClient.Models.DTOs;

namespace Airline.WebClient.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requestDTO);
    }
}
