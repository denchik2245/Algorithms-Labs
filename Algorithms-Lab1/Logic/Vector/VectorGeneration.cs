using System;

namespace MyLibrary.Logic.Vector
{
    public class VectorGenerator : IVectorGenerator
    {
        private static readonly Random rand = new Random();
        
        public int[] GenerateRandomVector(int n, int minValue = 1, int maxValue = 2000)
        {
            if (n <= 0)
                throw new ArgumentException("Размер вектора должен быть положительным числом.", nameof(n));

            int[] vector = new int[n];
            for (int i = 0; i < n; i++)
            {
                vector[i] = rand.Next(minValue, maxValue + 1);
            }

            return vector;
        }
    }
}