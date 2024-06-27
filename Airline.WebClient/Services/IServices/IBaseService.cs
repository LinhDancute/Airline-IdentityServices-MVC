using Airline.WebClient.Models.DTOs;

namespace Airline.WebClient.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDTO> SendAsyncCouponAPI(RequestDTO requestDTO);
        Task<ResponseDTO> SendAsyncScheduleAPI(RequestDTO requestDTO);

    }
}
