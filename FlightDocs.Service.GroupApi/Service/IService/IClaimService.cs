namespace FlightDocs.Service.GroupApi.Service.IService
{
    public interface IClaimService
    {
        string? GetCurrentUserId();
        string GetRole();
    }
}
