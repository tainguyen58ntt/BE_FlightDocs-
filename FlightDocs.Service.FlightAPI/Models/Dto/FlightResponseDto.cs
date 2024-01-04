using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace FlightDocs.Service.FlightAPI.Models.Dto
{
    public class FlightResponseDto
    {
        [JsonPropertyName("Fligh No")]
        public string FlightId { get; set; }
        [JsonPropertyName("Departure Airport")]
        public string DepartureAirport { get; set; }
        [JsonPropertyName("Arrival Airport")]
        public string ArrivalAirport { get; set; }
        [JsonPropertyName("Departure DateTime")]
        public DateTime DepartureDateTime { get; set; }
        [JsonPropertyName("Arrival DateTime")]
        public DateTime ArrivalDateTime { get; set; }


        //[JsonPropertyName("Total Documents")]
        //public int TotalDocuments { get; set; }
    }
}
