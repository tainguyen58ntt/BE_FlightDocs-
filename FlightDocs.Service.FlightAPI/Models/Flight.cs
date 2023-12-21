using System.ComponentModel.DataAnnotations;

namespace FlightDocs.Service.FlightAPI.Models
{
    public class Flight
    {
        [Key]
        public string FlightId { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
       
    }
}
