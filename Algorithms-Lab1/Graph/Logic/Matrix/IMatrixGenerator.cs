namespace Algorithms_Lab1.Logic.Matrix
{
    public interface IMatrixGenerator
    {
        static abstract int[,] GenerateRandomSquareMatrix(int size, int minvalue, int maxvalue);
    }
}
