namespace FlightDocs.Service.FlightAPI.Models.Dto
{
    public class FlightResponseDto
    {
        public string FlightId { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
    }
}
