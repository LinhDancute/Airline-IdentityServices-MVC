namespace Airline.Services.ScheduleAPI.Models.DTOs
{
    public class ResponsesDTO
    {
        public object? Result { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
