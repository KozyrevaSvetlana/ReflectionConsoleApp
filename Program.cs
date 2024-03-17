using ReflectionConsoleApp.Models;
using System.Diagnostics;
using System.Text.Json;

namespace ReflectionConsoleApp
{
    internal class Program
    {
        private static int count = 10_000;
        private static string path = "../../../";
        private static string fileName = "MyClassExample.txt";
        static void Main(string[] args)
        {
            // пункты 1 - 8
            GetStringFromClassF();
            SaveStringFromClassF();
            var data = LoadStringFromFile();
            if(string.IsNullOrEmpty(data))
                throw new ArgumentNullException("Нет данных из файла");
            GetClassFromString();
            //GetClassFromIni();
            //GetClassFromCSV();
        }


        /// <summary>
        /// Сохранить класс MyClass в файле проекта
        /// </summary>
        private static void SaveStringFromClassF()
        {
            var myClass = new MyClass("Светлана");
            var data = JsonSerializer.Serialize(myClass);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, fileName), true))
            {
                outputFile.WriteLine(data);
            }
        }

        /// <summary>
        /// Сохранить класс MyClass в файле проекта
        /// </summary>
        private static string? LoadStringFromFile()
        {
            String line = string.Empty;
            using (StreamReader sr = new StreamReader($"{path}\\{fileName}"))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    Console.WriteLine(line);
                }
            }
            return line;
        }

        /// <summary>
        /// загрузка данных из строки в экземпляр любого класса
        /// </summary>
        private static void GetClassFromString()
        {
            var classF = new F();
            var mytimer = new Stopwatch();
            Console.WriteLine("Сериализуемый класс: class F { int i1, i2, i3, i4, i5; }");
            Console.WriteLine($"количество замеров: {count} итераций");
            Console.WriteLine("мой рефлекшен:");
            mytimer.Start();
            for (int i = 0; i < count; i++)
            {

            }
            mytimer.Stop();
            TimeSpan timeTaken = mytimer.Elapsed;
            Console.WriteLine($"Время на сериализацию = {timeTaken.Milliseconds} мс");
            Console.WriteLine("Время на десериализацию = {} мс");
            Console.WriteLine();

            Console.WriteLine($"стандартный механизм (NewtonsoftJson):");
            var timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < count; i++)
            {

            }
            timer.Stop();
            TimeSpan timeJsonTaken = timer.Elapsed;
            Console.WriteLine("Время на сериализацию = {} мс");
            Console.WriteLine("Время на десериализацию = {} мс");
        }

        /// <summary>
        /// загрузка данных из csv-файла в экземпляр любого класса
        /// </summary>
        private static void GetClassFromCSV()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// загрузка данных из ini-файла в экземпляр любого класса
        /// </summary>
        private static void GetClassFromIni()
        {
            throw new NotImplementedException();
        }

        private static void GetStringFromClassF()
        {
            var classF = new F();

            var mytimer = new Stopwatch();
            mytimer.Start();
            for (int i = 0; i < count; i++)
            {
                MySerializer.Serialize(classF);
            }
            mytimer.Stop();
            TimeSpan timeTaken = mytimer.Elapsed;
            Console.WriteLine("Мой метод занял: " + timeTaken.ToString(@"m\:ss\.fff"));
            Console.WriteLine();

            var timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < count; i++)
            {
                JsonSerializer.Serialize(classF);
            }
            timer.Stop();
            TimeSpan timeJsonTaken = timer.Elapsed;
            Console.WriteLine("JsonSerializer занял: " + timeJsonTaken.ToString(@"m\:ss\.fff"));
            Console.WriteLine();
            var result = (timeTaken - timer.Elapsed).ToString(@"m\:ss\.fff");
            Console.WriteLine($"Разница - {result}");
        }
    }
}

//9.Написать десериализацию / загрузку данных из строки (ini/csv-файла) в экземпляр любого класса

//10. Замерить время на десериализацию

//11. Общий результат прислать в чат с преподавателем в системе в таком виде: