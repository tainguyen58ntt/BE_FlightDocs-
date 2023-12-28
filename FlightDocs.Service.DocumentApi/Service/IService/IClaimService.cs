namespace FlightDocs.Service.DocumentApi.Service.IService
{
    public interface IClaimService
    {
        string? GetCurrentUserId();
        string GetRole();
    }
}
