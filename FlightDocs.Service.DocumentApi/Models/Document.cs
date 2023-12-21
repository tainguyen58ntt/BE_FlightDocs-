namespace FlightDocs.Service.DocumentApi.Models
{
    public class Document
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string FlightId { get; set; }
        public decimal LastVersion { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }
    }
}
