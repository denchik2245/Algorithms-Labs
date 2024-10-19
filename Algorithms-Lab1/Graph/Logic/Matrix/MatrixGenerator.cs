namespace Algorithms_Lab1.Logic.Matrix
{
    public class MatrixGenerator : IMatrixGenerator
    {
        private static readonly Random rand = new Random();

        public static int[,] GenerateRandomSquareMatrix(int size, int minvalue, int maxvalue)
        {
            if (size <= 0)
                throw new ArgumentException("Число строк и столбцов квадратной матрицы должен быть положительным числом.", nameof(size));

            int[,] matrix = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = rand.Next(minvalue, maxvalue + 1);
                }
            }

            return matrix;
        }

        public static int[,] OptimizedMultiplyMatrices(int[,] matrixA, int[,] matrixB)
        {
            int n = matrixA.GetLength(0);

            if (n != matrixB.GetLength(0) || n != matrixB.GetLength(1))
                throw new ArgumentException("Обе матрицы должны быть квадратными и одинакового размера.");

            int[,] result = new int[n, n];

            Span<int> rowA = stackalloc int[n];

            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < n; k++)
                {
                    rowA[k] = matrixA[i, k];
                }

                for (int j = 0; j < n; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < n; k++)
                    {
                        sum += rowA[k] * matrixB[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }
    }
}

