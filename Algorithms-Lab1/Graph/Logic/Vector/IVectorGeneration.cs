namespace Algorithms_Lab1.Logic.Vector
{
    public interface IVectorGenerator
    {
        static abstract int[] GenerateRandomVector(int n, int minValue = 1, int maxValue = 2000);
    }
}