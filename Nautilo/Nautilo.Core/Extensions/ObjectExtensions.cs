using Newtonsoft.Json;

namespace Nautilo.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static string AsJson(this object objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }
    }
}
