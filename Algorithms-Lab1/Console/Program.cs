using System;
using MyVectorLibrary;

namespace MyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 10; // Размер вектора
            double x = 1.5; // Значение x для многочлена

            // Генерация случайного вектора
            int[] v = Logic.GenerateRandomVector(n);

            Console.WriteLine("Исходный вектор:");
            foreach (var value in v)
            {
                Console.Write($"{value} ");
            }
            Console.WriteLine();

            // Использование постоянной функции
            int constant = Logic.ConstantFunction();
            Console.WriteLine($"\nПостоянная функция возвращает: {constant}");

            // Вычисление суммы элементов
            int sum = Logic.SumElements(v);
            Console.WriteLine($"Сумма элементов: {sum}");

            // Вычисление произведения элементов
            long product = Logic.ProductElements(v);
            Console.WriteLine($"Произведение элементов: {product}");

            // Вычисление многочлена прямым методом
            double polynomialDirect = Logic.EvaluatePolynomialDirect(v, x);
            Console.WriteLine($"Многочлен (прямой метод) при x = {x}: {polynomialDirect}");

            // Вычисление многочлена методом Горнера
            double polynomialHorner = Logic.EvaluatePolynomialHorner(v, x);
            Console.WriteLine($"Многочлен (метод Горнера) при x = {x}: {polynomialHorner}");

            // Сортировка пузырьком
            int[] bubbleSorted = (int[])v.Clone();
            Logic.BubbleSort(bubbleSorted);
            Console.WriteLine("\nВектор после пузырьковой сортировки:");
            foreach (var value in bubbleSorted)
            {
                Console.Write($"{value} ");
            }
            Console.WriteLine();

            // Быстрая сортировка
            int[] quickSorted = (int[])v.Clone();
            Logic.QuickSort(quickSorted);
            Console.WriteLine("\nВектор после быстрой сортировки:");
            foreach (var value in quickSorted)
            {
                Console.Write($"{value} ");
            }
            Console.WriteLine();

            // Гибридная сортировка (Timsort)
            int[] timSorted = (int[])v.Clone();
            Logic.TimSort(timSorted);
            Console.WriteLine("\nВектор после гибридной сортировки:");
            foreach (var value in timSorted)
            {
                Console.Write($"{value} ");
            }
            Console.WriteLine();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
