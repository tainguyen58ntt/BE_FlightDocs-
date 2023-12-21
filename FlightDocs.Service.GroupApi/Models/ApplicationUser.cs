namespace FlightDocs.Service.GroupApi.Models
{
    public class ApplicationUser
    {
        public string Id { get;set; }

        public string Name { get; set; }

        public int GroupId { get; set; }

        //
        public Group Group { get; set; }
    }
}
