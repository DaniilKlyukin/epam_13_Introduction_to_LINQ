using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroductionToLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"B:\StudentsTestsResults.dat";

            try
            {
                // создаем объект BinaryReader
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    // пока не достигнут конец файла
                    // считываем каждое значение из файла
                    while (reader.PeekChar() > -1)
                    {
                        var studentName = reader.ReadString();
                        var testName = reader.ReadString();
                        var testDate = DateTime.FromBinary(reader.ReadInt64());
                        var mark = reader.ReadInt32();

                        Console.WriteLine("Имя: {0} \t тест: {1} \t дата: {2} \t оценка: {3}.",
                            studentName, testName, testDate, mark);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }

        private void AddData(string path)
        {
            StudentTestResult[] results =
{
                new StudentTestResult("Peter","Test1",DateTime.Now.AddMinutes(-39),2),
                new StudentTestResult("Peter","Test2",DateTime.Now.AddMinutes(-20),5),
                new StudentTestResult("Peter","Test3",DateTime.Now.AddMinutes(-11),5),
                new StudentTestResult("Vladimir","Test2",DateTime.Now.AddDays(-3),3),
                new StudentTestResult("Vladimir","Test1",DateTime.Now.AddDays(-3.4),4),
                new StudentTestResult("Vladimir","Test3",DateTime.Now.AddDays(-3.9),5),
                new StudentTestResult("Elena","Test3",DateTime.Now.AddHours(-1),4),
                new StudentTestResult("Elena","Test1",DateTime.Now.AddHours(-2.4),5),
                new StudentTestResult("Elena","Test2",DateTime.Now.AddHours(-3.4),5),
                new StudentTestResult("Boris","Test1",DateTime.Now.AddHours(-17),4),
                new StudentTestResult("Boris","Test2",DateTime.Now.AddHours(-16),4),
                new StudentTestResult("Boris","Test3",DateTime.Now.AddHours(-15),4),
                new StudentTestResult("Egor","Test3",DateTime.Now.AddHours(-15),3),
                new StudentTestResult("Egor","Test2",DateTime.Now.AddHours(-57.6),5),
                new StudentTestResult("Egor","Test1",DateTime.Now.AddHours(-5.7),4),
                new StudentTestResult("Igor","Test3",DateTime.Now.AddHours(-12.1),1),
                new StudentTestResult("Igor","Test1",DateTime.Now.AddHours(-5),4),
                new StudentTestResult("Igor","Test2",DateTime.Now.AddHours(-20),5),
            };

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                foreach (var s in results)
                {
                    writer.Write(s.StudentName);
                    writer.Write(s.TestName);
                    writer.Write(s.TestDate.ToBinary());
                    writer.Write(s.Mark);
                }
            }
        }
    }
}
