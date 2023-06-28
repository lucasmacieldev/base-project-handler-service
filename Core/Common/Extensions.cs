using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Linq;


namespace Common
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes?.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description : value.ToString() ?? value.ToString();
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
        }

        public static string ToJson(this object obj, bool igonoreNull = false)
        {
            if (igonoreNull)
                return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Utc });

            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Utc });
        }

        public static string ToIndentedJson(this object obj, bool igonoreNull = false)
        {
            if (igonoreNull)
                return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Utc });

            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Utc });
        }

        public static T FromJson<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
        }

        public static bool TryParseJson<T>(this string value, out T result)
        {
            try
            {
                result = JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
                return true;
            }
            catch
            {
                result = default(T);
                return false;
            }
        }

        public static Guid ToGuid(this string value)
        {
            return new Guid(value);
        }

        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        public static string UnMask(this string value)
        {
            return value.HasValue() ? value.Replace("-", "").Replace(".", "").Replace("/", "") : value;
        }

        public static string GetDisplayText(this Enum value)
        {
            string itemDescription;
            var attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DisplayTextAttribute), false);
            if (!attributes.Any())
            {
                attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
                itemDescription = attributes.Any() ? ((DescriptionAttribute)attributes[0]).Description : value.ToString();
            }
            else
                itemDescription = ((DisplayTextAttribute)attributes[0]).DisplayText;

            return itemDescription;
        }

        //public static IServiceCollection RegisterOptions<T>(this IServiceCollection services, string configurationPath = null) where T : class
        //{
        //    services.AddOptions<T>().Configure(cfg => Configuration.GetConfiguration().Bind(configurationPath ?? typeof(T).Name, cfg));
        //    return services;
        //}

    }
}
