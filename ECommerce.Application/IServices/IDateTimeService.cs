namespace ECommerce.Application.IServices;

public interface IDateTimeService
{
    DateTime GetDateTime();
    DateTime UTCDateTime();
    DateTime LocalDateTime();
    DateTime UnixTimeStampToDateTime(long unixTimeStamp);
}
