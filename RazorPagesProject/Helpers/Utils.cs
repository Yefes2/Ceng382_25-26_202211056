using System.text.Json;

namespace RazorPagesProject.Helpers
{
    public sealed class Utils
    {
        private static readonly Utils _instance = new Utils();

        public static Utils Instance => _instance;

        private Utils() { }

        public string ExportToJson<T>(List<T> data, List<string> selectedColumns = null)
        {
            if (selectedColumns == null || selectedColumns.Count == 0)
            {
                return JsonSerializer.Serialize(data);
            }

            // Filter to only selected columns dynamically
            var filteredData = data.Select(item =>
            {
                var dict = new Dictionary<string, object>();
                var props = typeof(T).GetProperties();
                foreach (var prop in props)
                {
                    if (selectedColumns.Contains(prop.Name))
                    {
                        dict[prop.Name] = prop.GetValue(item);
                    }
                }
                return dict;
            }).ToList();

            return JsonSerializer.Serialize(filteredData);
        }
    }
}