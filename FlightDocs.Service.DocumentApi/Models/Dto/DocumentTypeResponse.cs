using System.Text.Json.Serialization;

namespace FlightDocs.Service.DocumentApi.Models.Dto
{
    public class DocumentTypeResponse
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        [JsonPropertyName("Creator")]
        public string CreateBy { get; set; }

        public int NumberOfGroupPermissions { get; set; }

        //public IEnumerable<Document> Documents { get; set; }
    }
}
