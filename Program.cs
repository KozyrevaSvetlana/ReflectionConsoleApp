using ReflectionConsoleApp.Models;
using System.Diagnostics;
using System.Text.Json;

namespace ReflectionConsoleApp
{
    internal class Program
    {
        private static int count = 10_000;
        static void Main(string[] args)
        {
            GetStringFromClassF();
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

//Сериализуемый класс: class F { int i1, i2, i3, i4, i5; }

//код сериализации-десериализации: ...

//количество замеров: 1000 итераций

//мой рефлекшен:

//Время на сериализацию = 100 мс

//Время на десериализацию = 100 мс

//стандартный механизм (NewtonsoftJson):

//Время на сериализацию = 100 мс

//Время на десериализацию = 100 мс