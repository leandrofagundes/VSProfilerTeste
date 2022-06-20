using System;
using System.IO;
using System.Linq;
using System.Text;

namespace StressProfiler
{
    internal class Program
    {
        private static Random random = new Random();
        private static StringBuilder stringao = new StringBuilder();

        static void Main(string[] args)
        {
            var dirName = @"c:\temp\stress";
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            DeleteFiles(dirName);

            CreateFiles(dirName);

            DeleteLixo();

            Console.WriteLine("Finished");
        }

        private static void DeleteLixo()
        {
            Console.WriteLine("Deleting lixo");

            foreach (var file in Directory.GetFiles(@"c:\temp\lixo"))
            {
                Console.WriteLine($"Deleting lixo {file}");
                if (File.Exists(file))
                    File.Delete(file);
            }
        }

        private static void CreateFiles(string dirName)
        {
            Console.WriteLine("Creating");
            for (int i = 0; i < 10000; i++)
            {
                var fileName = Path.Combine(dirName, $"{i}.txt");

                Clearstringao(i);

                var randomString = GenerateNextChar();
                stringao.AppendLine(stringao.ToString());
                stringao.AppendLine(randomString);

                CheckFile(fileName);

                //CreateFile(fileName);

            }
        }

        private static void Clearstringao(int i)
        {
            if (i % 20 == 0)
            {
                Console.WriteLine($"Clearing {i}");
                stringao.Clear();
                for (int j = 0; j < 1; j++)
                {
                    if (!Directory.Exists(@"c:\temp\lixo"))
                        Directory.CreateDirectory(@"c:\temp\lixo");

                    File.Create(@$"c:\temp\lixo\{i}{j}.txt");
                    Console.WriteLine($"Creating lixo {i}{j}");
                }
            }

        }

        private static void CreateFile(string fileName)
        {
            File.WriteAllText(fileName, stringao.ToString());
        }

        private static void CheckFile(string fileName)
        {
            if (File.Exists(fileName))
                DeleteFile(fileName);
        }

        private static string GenerateNextChar()
        {
            var minNumber = random.Next(1, 3);
            var maxNumber = random.Next(4, 10);
            var numberOfChars = random.Next(minNumber, maxNumber);
            return RandomString(numberOfChars);
        }

        private static void DeleteFiles(string dirName)
        {
            Console.WriteLine("Deleting");
            foreach (var file in Directory.GetFiles(dirName))
            {
                var fileValue = GetFileInteger(file);
                if (!IsValidFile(fileValue))
                    DeleteFile(file);
            }
        }

        private static bool IsValidFile(int fileValue)
        {
            if (fileValue % 2 == 0)
                return true;
            else
                return false;
        }

        private static int GetFileInteger(string file)
        {
            var integer = Path.GetFileNameWithoutExtension(file);
            var value = int.Parse(integer);
            return value;
        }

        private static void DeleteFile(string file)
        {
            File.Delete(file);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
