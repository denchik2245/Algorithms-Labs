using System.Diagnostics;
using System.Reflection;
using MyVectorLibrary.Sorters;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Type[] availableAlgorithms = GetAllAlgorithms();
            Console.WriteLine("Нажмите номер соответствующий алгоритму который вы хотите выполнить:");

            for (int i = 0; i < availableAlgorithms.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {availableAlgorithms[i].Name}");
            }

            if (int.TryParse(Console.ReadLine(), out int algorithmChoice) && algorithmChoice >= 1 && algorithmChoice <= availableAlgorithms.Length)
            {
                Type chosenAlgorithm = availableAlgorithms[algorithmChoice - 1];
                Console.WriteLine($"Вы выбрали: {chosenAlgorithm.Name}");
                ExecuteTests(chosenAlgorithm);
            }
            else
            {
                Console.WriteLine("Неверный выбор алгоритма.");
            }
        }

        static Type[] GetAllAlgorithms()
        {
            var algorithms = GetTypesFromNamespace("MyLibrary.Logic.Algorithms");
            var operations = GetTypesFromNamespace("MyLibrary.Logic.Operation");
            return algorithms.Concat(operations).ToArray();
        }

        static Type[] GetTypesFromNamespace(string @namespace)
        {
            return Assembly.GetAssembly(typeof(ISorter))
                           .GetTypes()
                           .Where(t => t.Namespace == @namespace && !t.IsInterface && !t.IsAbstract)
                           .ToArray();
        }

        static void ExecuteTests(Type algorithmType)
        {
            Console.WriteLine("Введите количество тестов (от 5 до 20):");
            if (int.TryParse(Console.ReadLine(), out int testCount) && testCount >= 5 && testCount <= 20)
            {
                object algorithmInstance = Activator.CreateInstance(algorithmType);
                MethodInfo methodToTest = algorithmType.GetMethod("Sort") ?? algorithmType.GetMethod("Calculate");

                if (methodToTest == null)
                {
                    Console.WriteLine("Метод для выполнения не найден.");
                    return;
                }

                double totalTime = 0;
                for (int i = 1; i <= testCount; i++)
                {
                    var randomVector = GenerateRandomVector(1000);
                    var stopwatch = Stopwatch.StartNew();

                    methodToTest.Invoke(algorithmInstance, new object[] { randomVector });

                    stopwatch.Stop();
                    double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                    totalTime += elapsedSeconds;

                    Console.WriteLine($"Тест {i}: {elapsedSeconds:F5} сек");
                }

                double averageTime = totalTime / testCount;
                Console.WriteLine($"Среднее время выполнения: {averageTime:F5} сек");
            }
            else
            {
                Console.WriteLine("Некорректное количество тестов.");
            }
        }

        static int[] GenerateRandomVector(int n)
        {
            Random rand = new Random();
            return Enumerable.Range(0, n).Select(_ => rand.Next(1, 100)).ToArray();
        }
    }
}
