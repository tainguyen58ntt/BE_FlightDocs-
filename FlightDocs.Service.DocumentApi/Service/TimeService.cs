using FlightDocs.Service.DocumentApi.Service.IService;

namespace FlightDocs.Service.DocumentApi.Service
{
    public class TimeService : ITimeService
    {
        //public DateTime GetCurrentTime()
        //{
        //	return DateTime.UtcNow;
        //}

        public DateTime GetCurrentTimeInVietnam()
        {
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
            return vietnamTime;
        }
    }
}
