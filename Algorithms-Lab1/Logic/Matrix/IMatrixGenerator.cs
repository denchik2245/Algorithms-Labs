namespace MyLibrary.Logic.Matrix
{
    public interface IMatrixGenerator
    {
        static abstract int[,] GenerateRandomSquareMatrix(int size, int minvalue, int maxvalue);
    }
}
