using System.Text.Json;

namespace backend.Utils
{
    public class JsonManager
    {
        // Return generic object from json string
        public T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json)!;
        }

        // Retirn json string from generic object
        public string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
