namespace MyLibrary.Logic.Vector
{
    public interface IVectorGenerator
    {
        int[] GenerateRandomVector(int n, int minValue = 1, int maxValue = 2000);
    }
}
