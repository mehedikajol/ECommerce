using ECommerce.Application.IServices;

namespace ECommerce.Infrastructure.Services;

internal class DateTimeService : IDateTimeService
{
    public DateTime GetDateTime()
    {
        return DateTime.Now;
    }

    public DateTime UTCDateTime()
    {
        return DateTime.UtcNow;
    }

    public DateTime LocalDateTime()
    {
        var now = DateTime.Now;
        return now.ToLocalTime();
    }

    public DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
        return dateTime;
    }

    
}
