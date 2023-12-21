using Microsoft.AspNetCore.Identity;

namespace FlightDocs.Serivce.AuthApi.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string Name { get; set; }
    }
}
