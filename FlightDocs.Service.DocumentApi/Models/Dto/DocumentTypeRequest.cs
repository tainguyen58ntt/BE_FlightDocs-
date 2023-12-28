namespace FlightDocs.Service.DocumentApi.Models.Dto
{
    public class DocumentTypeRequest
    {
        public string Type { get; set; }
        public string Note { get; set; }
        //public DateTime CreateDate { get; set; }
        //public string CreateBy { get; set; }

        public List<GroupPermissionRequestDto>? GroupPermissionRequestDtos { get; set; }

    }
}
