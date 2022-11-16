using Microsoft.AspNetCore.Http;

namespace QLBikeStores.Helpers
{
    public static class ExtensionHelper
    {
        public static string ToVnd(this double value)
        {
            return $"{value:#,##0.00}đ";
        }
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, System.Text.Json.JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
    }
}
