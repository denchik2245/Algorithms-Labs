using System.Numerics;

namespace MyLibrary.Logic.Operation
{
    public class MultiplicationOfElements
    {
        public BigInteger Calculate(int[] vector)
        {
            if (vector == null || vector.Length == 0)
                throw new ArgumentException("Массив не должен быть пустым.", nameof(vector));

            BigInteger result = 1;
            foreach (int val in vector)
            {
                result *= val;
            }
            return result;
        }
    }

}