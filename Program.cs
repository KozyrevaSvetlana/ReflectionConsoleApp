using ReflectionConsoleApp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReflectionConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var classF = new F();
            var typeF = typeof(F);
            var fields = typeF.GetFields();
            var properties = typeF.GetProperties();
            var constructors = typeF.GetConstructors();
            var methods = typeF.GetMethods();
            var events = typeF.GetEvents();

            var result = MySerializer.Serialize(classF);
            Console.WriteLine(result);

            Console.WriteLine("Стандартный json");
            Console.WriteLine(JsonSerializer.Serialize(classF));
        }
    }
}

//1.Написать сериализацию свойств или полей класса в строку

//2. Проверить на классе: class F 

//3.Замерить время до и после вызова функции (для большей точности можно сериализацию сделать в цикле 100-100000 раз)

//4. Вывести в консоль полученную строку и разницу времен

//5. Отправить в чат полученное время с указанием среды разработки и количества итераций

//6. Замерить время еще раз и вывести в консоль сколько потребовалось времени на вывод текста в консоль

//7. Провести сериализацию с помощью каких-нибудь стандартных механизмов (например в JSON)

//8. И тоже посчитать время и прислать результат сравнения

//9. Написать десериализацию/загрузку данных из строки (ini/csv-файла) в экземпляр любого класса

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
