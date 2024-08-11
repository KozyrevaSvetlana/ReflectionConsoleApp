using System.Dynamic;
using System.Reflection;
using System.Text;

namespace ReflectionConsoleApp.Models
{
    public static class MySerializer
    {
        private static Dictionary<Type, int> typeDict = new()
        {
            {typeof(int), 0},
            {typeof(string), 1},
            {typeof(char), 2},
            {typeof(bool), 3},
            {typeof(Enum), 4},
            {typeof(DateTime), 5},
        };
        public static string Serialize<T>(T _object)
        {
            if (_object == null)
                throw new ArgumentNullException(nameof(_object));

            var result = new StringBuilder();
            result.Append("{");
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead);
            foreach (var property in properties)
            {
                property.Serialize(_object, result);
            }
            result.Length--;
            result.Append("}");
            return result.ToString();
        }

        public static T? DeserializeObject<T>(string input) where T : new()
        {
            var model = new T();
            string[] parts = input.Split(',');

            var properties = typeof(T).GetProperties();

            for (int i = 0; i < properties.Length && i < parts.Length; i++)
            {
                var value = parts[i].Trim();
                var property = properties[i];

                if (property.CanWrite)
                {
                    object? convertedValue = null;
                    switch (property.PropertyType.Name)
                    { 
                        case "Boolean":
                            convertedValue = ConvertToBoolean(value);
                            break;
                        case "DateTime":
                            if (DateTime.TryParse(value, out DateTime parsedDate))
                                convertedValue = parsedDate;
                            break;
                        default:
                            convertedValue = Convert.ChangeType(value, property.PropertyType);
                            break;
                    }
                    property.SetValue(model, convertedValue);
                }
            }
            return model;
        }

        private static bool? ConvertToBoolean(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            if (string.Equals(value, "true", StringComparison.OrdinalIgnoreCase))
                return true;
            else if (string.Equals(value, "false", StringComparison.OrdinalIgnoreCase))
                return false;

            return null;
        }

        private static StringBuilder Serialize(this PropertyInfo property, object _object, StringBuilder builder)
        {
            if (property.PropertyType == null)
                return builder;

            switch (typeDict[property.PropertyType])
            {
                case 1:
                    builder.Append($"\"{property.Name}\":\"{property.GetValue(_object)}\",");
                    break;
                case 0:
                case 2:
                    builder.Append($"\"{property.Name}\":{property.GetValue(_object)},");
                    break;
                case 3:
                    builder.Append($"\"{property.Name}\":{property.GetValue(_object).ToString().ToLower()},");
                    break;
                case 4:
                    builder.Append($"\"{property.Name}\":{(int)property.GetValue(_object)!},");
                    break;
                case 5:
                    builder.Append($"\"{property.Name}\":\"{((DateTime)property.GetValue(_object)).ToString("o")}\",");
                    break;
                default:
                    builder.AppendLine("{");
                    property.Serialize(_object, builder);
                    builder.AppendLine("},");
                    break;
            }
            return builder;
        }
    }
}
