namespace Airline.ModelsService.Models.DTOs.Schedule
{
    public class ResponsesDTO
    {
        public object? Result { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
