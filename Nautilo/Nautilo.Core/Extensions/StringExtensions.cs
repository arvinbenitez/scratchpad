using Newtonsoft.Json;

namespace Nautilo.Core.Extensions
{
    public static class StringExtensions
    {
        public static T DeserializeFromJson<T>(this string value)
        {
            var type = value.GetType();

            if (!type.IsSerializable)
            {
                return default(T);
            }

            var serializeType = typeof(T);

            var result = JsonConvert.DeserializeObject(value, serializeType);

            return (T)result;
        } 
    }
}