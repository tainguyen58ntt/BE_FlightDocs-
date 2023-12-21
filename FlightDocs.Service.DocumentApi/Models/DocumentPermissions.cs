namespace FlightDocs.Service.DocumentApi.Models
{
    public class DocumentPermissions
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public int GroupId { get; set; }
        public string PermissionLevel { get; set; }


        public Document Document { get; set; }
        public Group Group { get; set; }

    }
}
