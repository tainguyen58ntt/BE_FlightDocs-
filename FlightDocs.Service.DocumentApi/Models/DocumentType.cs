namespace FlightDocs.Service.DocumentApi.Models
{
    public class DocumentType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime CreateDate { get; set; }    
        public string CreateBy { get; set; }

        public IEnumerable<Document> Documents { get; set; }
    }
}
