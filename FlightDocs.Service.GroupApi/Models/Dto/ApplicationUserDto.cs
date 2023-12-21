using Newtonsoft.Json;

namespace FlightDocs.Service.GroupApi.Models.Dto
{
    public class ApplicationUserDto
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
