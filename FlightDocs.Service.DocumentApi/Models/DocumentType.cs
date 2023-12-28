using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FlightDocs.Service.DocumentApi.Models
{
    public class DocumentType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }    
        public string CreateBy { get; set; }

        public IEnumerable<Document> Documents { get; set; }

        public static implicit operator DocumentType(EntityEntry<DocumentType> v)
        {
            throw new NotImplementedException();
        }
    }
}
