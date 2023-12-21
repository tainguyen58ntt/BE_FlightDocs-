using FlightDocs.Serivce.AuthApi.Models;

namespace FlightDocs.Serivce.AuthApi.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
