using FlightDocs.Serivce.AuthApi.Models.Dto.Email;

namespace FlightDocs.Serivce.AuthApi.Service.IService
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
