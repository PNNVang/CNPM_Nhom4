using Newtonsoft.Json;

namespace Dot_Net_ECommerceWeb.Extensions
{
    public static class SessionExtensions
    {
        // Phương thức lưu đối tượng vào Session dưới dạng JSON
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Phương thức lấy đối tượng từ Session và deserialize từ JSON
        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
