namespace Parentee_BE.DAL.Helpers;

public class TimeZoneHelper
{
    public static DateTime ToVietnamTime(DateTime utcDateTime)
    {
        var vnZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, vnZone);
    }
}