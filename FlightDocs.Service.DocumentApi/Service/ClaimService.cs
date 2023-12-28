using FlightDocs.Service.DocumentApi.Service.IService;
using System.Security.Claims;

namespace FlightDocs.Service.DocumentApi.Service
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _contextAccessor;


        public ClaimService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string? GetCurrentUserId()
        {
            var id = _contextAccessor.HttpContext?.User?.FindFirst("Id")?.Value;
            //return id is null ? -1 : id;
            return id;
        }

        public string GetRole()
        {
            //var id = _contextAccessor.HttpContext?.User?.FindFirst("RoleId")?.Value;
            var role = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
            return role;
        }
    }
}
