namespace MyLibrary.Logic.Operation
{
    public class MultiplicationOfElements
    {
        public int Calculate(int[] vector)
        {
            return vector.Aggregate(1, (acc, val) => acc * val);
        }
    }
}