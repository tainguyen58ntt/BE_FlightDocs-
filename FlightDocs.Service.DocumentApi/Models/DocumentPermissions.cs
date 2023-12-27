namespace FlightDocs.Service.DocumentApi.Models
{
    public class DocumentPermissions
    {
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public int GroupId { get; set; }
        public string PermissionLevel { get; set; }


        public DocumentType DocumentType { get; set; }
        public Group Group { get; set; }

    }
}
