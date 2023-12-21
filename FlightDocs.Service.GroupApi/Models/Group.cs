namespace FlightDocs.Service.GroupApi.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Note { get; set; }

        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }

        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }

    }
}
