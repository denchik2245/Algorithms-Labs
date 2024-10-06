using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Logic.Matrix
{
    public class MatrixGenerator
    {
        private static readonly Random rand = new Random();
        public static int[,] GenerateRandomSquareMatrix(int size, int minvalue, int maxvalue)
        {
            if (size <= 0)
                throw new ArgumentException("Число строк и столбцов квадратной матрицы должен быть положительным числом.", nameof(size));

            int[,] matrix = new int[size, size];

            for (int i = 0;i < size; i++)
            {
                for (int j = 0;j < size; j++)
                {
                    matrix[i,j] = rand.Next(minvalue, maxvalue + 1);
                }
            }

            return matrix;
        }
    }
}
