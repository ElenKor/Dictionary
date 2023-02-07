using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using CreateDictionary;
using System.Diagnostics;


// (example) входной файл: /Users/elina/Projects/task2/task2/bin/toltoy.txt
// выходной файл: /Users/elina/Projects/task2/task2/bin/Test.txt
namespace Program
{

    public static class ReadText
    {
        public static string ReadTextFromFile()
        {
            try
            {
                Console.WriteLine("Введите путь к файлу для чтения данных");
                string path = CheckPath.CorrectPath(Console.ReadLine());
                string text = File.ReadAllText(path);
                return text;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }
    }

    public static class CheckPath
    {
        public static string CorrectPath(string path)
        {

            bool isCorrectPath = false;
            isCorrectPath = File.Exists(path);
            while (!isCorrectPath)
            {
                Console.WriteLine("Указан некорректный путь.");
                Console.WriteLine("Повторите попытку. Введите путь к файлу для чтения данных ");
                path = Console.ReadLine();
                isCorrectPath = File.Exists(path);
            }
            return path;
        }
    }

    public static class EntryText
    {
        public static void EntryTextInFile(Dictionary<string, int> dictionary)
        {
            try
            {
                Console.WriteLine("Введите путь к директории для сохранения файла с результами работы программы");
                string path = CheckPath.CorrectPath(Console.ReadLine());
                StreamWriter sw = new StreamWriter(path);
                //Вывод полученных данных в текстовый файл
                foreach (var pair in dictionary.OrderByDescending(x => x.Value))
                {
                    sw.WriteLine("{0}: {1}", pair.Key, pair.Value);

                }
                Console.WriteLine("Текстовый файл записан. Программа завершена.");
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {

            string text = ReadText.ReadTextFromFile();
            Stopwatch stopWatch = new Stopwatch();
        
            Type type = typeof(Class1);
            ConstructorInfo ctor = type.GetConstructor(new Type[] { });//поиск конструктора
            var obj = ctor.Invoke(new object[] { });//создание экземпляра класса
            stopWatch.Start();
            //вызов приватного метода createDictionary
           // var result = (Dictionary<string, int>)type.GetMethod("CreateDictionary", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj, new object[] { text});
            stopWatch.Stop();
            Console.WriteLine("Private method: " + stopWatch.ElapsedMilliseconds+" ms");
            
            
            MethodInfo mi = type.GetMethod("CreateDictionaryWithThread");
            var obj1 = ctor.Invoke(new object[] { });
            stopWatch.Restart();
            var result1 = (Dictionary<string, int>)mi.Invoke(obj, new object[] { text });
            stopWatch.Stop();
            Console.WriteLine("Public method: " + stopWatch.ElapsedMilliseconds+ " ms");
           

           // EntryText.EntryTextInFile(result);
            EntryText.EntryTextInFile(result1);

            

        }

       
    }
}