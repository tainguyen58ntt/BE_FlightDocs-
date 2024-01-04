using System.Text.Json.Serialization;

namespace FlightDocs.Service.DocumentApi.Models.Dto
{
    public class DocumentResponseAllDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }

        //[JsonPropertyName("Document type")]
        public string DocumentType { get; set; }
        //public byte[] FileData { get; set; }
        public string FlightId { get; set; }
        //public decimal LastVersion { get; set; } = 1.0M;
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        //public DateTime? UpdateDate { get; set; }
        //public string? UpdateBy { get; set; }
    }
}
