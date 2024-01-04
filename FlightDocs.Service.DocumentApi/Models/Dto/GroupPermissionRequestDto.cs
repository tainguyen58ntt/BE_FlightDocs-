using FlightDocs.Service.DocumentApi.Constraint;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlightDocs.Service.DocumentApi.Models.Dto
{
    public class GroupPermissionRequestDto
    {
        [JsonPropertyName("Group id")]
        public int GroupId { get; set; }

        [EnumDataType(typeof(PermissionLevel))]
        public string PermissionLevel { get; set; }
    }
}
