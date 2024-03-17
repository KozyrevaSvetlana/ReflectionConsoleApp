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
        };
        public static string Serialize<T>(T _object)
        {
            if(_object == null)
                throw new ArgumentNullException(nameof(_object));

            var result = new StringBuilder();
            result.Append("{");
            var type = typeof(T);
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead && p.CanWrite);
            foreach (var property in properties)
            {
                property.Serialize(_object, result);
            }
            result.Length--;
            result.Append("}");
            return result.ToString();
        }

        public static T? DeserializeObject1<T>(string value)
        {
            return (T?)Activator.CreateInstance(typeof(T));
        }

        private static StringBuilder Serialize(this PropertyInfo property, object _object, StringBuilder builder)
        {
            switch (typeDict[property.PropertyType])
            {
                case 0:
                case 1:
                case 2:
                    builder.Append($"\"{property.Name}\":{property.GetValue(_object)},");
                    break;
                case 3:
                    break;
                default:
                    property.Serialize(_object, builder);
                    break;
            }
            return builder;
        }
    }
}
