using ReflectionConsoleApp.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace ReflectionConsoleApp
{
    internal class Program
    {
        private static int count = 10_000;
        private static string path = "../../../";
        private static string fileName = "MyClassExample.txt";
        private static JsonSerializerOptions optionsCyrillic = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true,
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true
        };
        static void Main(string[] args)
        {
            SaveStringFromClassF();
            var data = LoadStringFromFile();
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException("Нет данных из файла");
            GetClassFromString(data);
        }


        /// <summary>
        /// Сохранить класс MyClass в файле проекта
        /// </summary>
        private static void SaveStringFromClassF()
        {
            var myClass = new MyClass("Светлана");
            var data = MySerializer.Serialize(myClass);
            Console.WriteLine(data);
            var dataJson = JsonSerializer.Serialize(myClass, optionsCyrillic);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, fileName), false, Encoding.GetEncoding("windows-1251")))
            {
                outputFile.WriteLine(data);
            }
        }

        /// <summary>
        /// Сохранить класс MyClass в файле проекта
        /// </summary>
        private static string? LoadStringFromFile()
        {
            var result = new StringBuilder();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (StreamReader sr = new StreamReader($"{path}\\{fileName}", Encoding.GetEncoding("windows-1251")))
            {
                while (!sr.EndOfStream)
                {
                    result.Append(sr.ReadLine());
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// загрузка данных из строки в экземпляр любого класса
        /// </summary>
        private static void GetClassFromString(string data)
        {
            var mytimer = new Stopwatch();
            mytimer.Start();
            Console.WriteLine("Сериализуемый класс: class F { int i1, i2, i3, i4, i5; }");
            Console.WriteLine($"количество замеров: {count} итераций");
            Console.WriteLine("мой рефлекшен:");
            var myClass = MySerializer.DeserializeObject<MyClass>(data);
            Console.WriteLine(myClass);
            for (int i = 0; i < count; i++)
            {
                myClass = MySerializer.DeserializeObject<MyClass>(data);
            }
            mytimer.Stop();
            TimeSpan timeTaken = mytimer.Elapsed;
            Console.WriteLine($"Время на сериализацию = {timeTaken.Milliseconds} мс");

            Console.WriteLine();

            Console.WriteLine($"стандартный механизм (NewtonsoftJson):");
            var timer = new Stopwatch();
            timer.Start();
            var myClassDefault = JsonSerializer.Deserialize<MyClass>(data, optionsCyrillic);
            for (int i = 0; i < count; i++)
            {
                myClassDefault = JsonSerializer.Deserialize<MyClass>(data, optionsCyrillic);
            }
            timer.Stop();
            TimeSpan timeJsonTaken = timer.Elapsed;
            Console.WriteLine($"Время на сериализацию = {timeJsonTaken.Milliseconds} мс");
            Console.WriteLine($"Разница = {timeTaken.Milliseconds - timeJsonTaken.Milliseconds}");
        }
    }
}