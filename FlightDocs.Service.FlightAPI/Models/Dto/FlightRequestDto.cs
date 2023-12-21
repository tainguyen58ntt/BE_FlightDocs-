namespace FlightDocs.Service.FlightAPI.Models.Dto
{
    public class FlightRequestDto
    {
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
    }
}
