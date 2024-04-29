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

        public static T? DeserializeObject<T>(string value)
        {
            return (T?)Activator.CreateInstance(typeof(T));
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
