using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IntroductionToLINQ
{
    public enum SortOrder
    {
        Ascending = 1,
        Descending = -1
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = @"B:\StudentsTestsResults.dat";

            var userArgs = GetInputArgumentsFromUser();

            var orderType = userArgs.Item1;
            var rowsCount = userArgs.Item2;
            var fieldName = userArgs.Item3;

            var data = GetData(path)
                .OrderBy(fieldName, orderType)
                .Take(rowsCount)
                .Select(d =>
            {
                Console.WriteLine($"Имя: {d.StudentName}\t Тест: {d.TestName}\t Дата: {d.TestDate}\t Оценка: {d.Mark}.");
                return d;
            }).ToList();

            Console.ReadLine();
        }

        private static Tuple<SortOrder, int, string> GetInputArgumentsFromUser()
        {
            var orderType = EnterSortOrder();

            var rowsLimit = EnterRowsLimit();

            var fieldName = EnterSortingField();

            return Tuple.Create(orderType, rowsLimit, fieldName);
        }

        private static List<StudentTestResult> GetData(string path)
        {
            var list = new List<StudentTestResult>();

            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        var studentName = reader.ReadString();
                        var testName = reader.ReadString();
                        var testDate = DateTime.FromBinary(reader.ReadInt64());
                        var mark = reader.ReadInt32();

                        list.Add(new StudentTestResult(studentName, testName, testDate, mark));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return list;
        }

        private static SortOrder EnterSortOrder()
        {
            Console.WriteLine("Выберите тип упорядочивания (1 - по возрастанию, -1 - по убыванию).");
            int parsedSortOrder;
            var parsed1 = int.TryParse(Console.ReadLine(), out parsedSortOrder);

            if (!parsed1 || Math.Abs(parsedSortOrder) != 1)
            {
                Console.WriteLine("Неверные входные данные, значение выставлено по умолчанию (по возрастанию).");
                return SortOrder.Ascending;
            }
            else
                return parsedSortOrder == 1 ? SortOrder.Ascending : SortOrder.Descending;
        }

        private static int EnterRowsLimit()
        {
            Console.WriteLine("Выберите ограничение по количеству строк (-1 - без ограничений).");
            int parsedRowsCount;
            var parsed2 = int.TryParse(Console.ReadLine(), out parsedRowsCount);

            if (!parsed2)
            {
                Console.WriteLine("Неверные входные данные, значение выставлено по умолчанию (без ограничений).");
                return int.MaxValue;
            }
            else
                return parsedRowsCount;
        }

        private static string EnterSortingField()
        {
            Console.WriteLine("Выберите по какому полю сортировать (1 - Имя студента, 2 - Название теста, 3 - Дата проведения теста, 4 - Оценка).");
            int parsedFieldNumber;
            var parsed3 = int.TryParse(Console.ReadLine(), out parsedFieldNumber);

            if (!parsed3)
            {
                Console.WriteLine("Неверные входные данные, значение будет выставлено по умолчанию (Имя студента).");
                return "StudentName";
            }
            else
                return GetFieldNameOnIndex(parsedFieldNumber);
        }

        private static string GetFieldNameOnIndex(int index)
        {
            switch (index)
            {
                case 1: return "StudentName";
                case 2: return "TestName";
                case 3: return "TestDate";
                case 4: return "Mark";
                default:
                    {
                        Console.WriteLine("Неверные входные данные, значение будет выставлено по умолчанию(Имя студента).");
                        return "StudentName";
                    }
            }
        }

        private static void AddData(string path)
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
