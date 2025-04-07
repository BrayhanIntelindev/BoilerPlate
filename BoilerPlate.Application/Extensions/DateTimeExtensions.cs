using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Application.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToUtc(this DateTime inputDate)
        {
            return DateTime.SpecifyKind(inputDate, DateTimeKind.Utc);
        }

        public static DateTime ToClientTimeZone(this DateTime utcDate, IHeaderDictionary headers)
        {
            if (headers.TryGetValue("Time-Zone", out var timeZoneHeader) && !string.IsNullOrEmpty(timeZoneHeader))
            {
                try
                {
                    TimeZoneInfo clientTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneHeader);
                    return TimeZoneInfo.ConvertTimeFromUtc(utcDate, clientTimeZone);
                }
                catch (TimeZoneNotFoundException)
                {
                    Console.WriteLine($"Zona horaria no válida: {timeZoneHeader}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al intentar convertir a la zona horaria: {timeZoneHeader} - Error {ex.Message}");
                }
            }
            return utcDate; 
        }
    }
}
