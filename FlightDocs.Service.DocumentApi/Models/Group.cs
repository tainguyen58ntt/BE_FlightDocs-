namespace FlightDocs.Service.DocumentApi.Models
{
    public class Group
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public string? Note { get; set; }

        public string CreateBy { get; set; }

    }
}
