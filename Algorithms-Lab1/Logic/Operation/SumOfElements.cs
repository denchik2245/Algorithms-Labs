using System.Numerics;

namespace MyLibrary.Logic.Operation
{
    public class SumOfElements
    {
        public BigInteger Calculate(int[] vector)
        {
            if (vector == null)
                throw new ArgumentNullException(nameof(vector), "Массив не должен быть null.");

            BigInteger sum = 0;
            foreach (int val in vector)
            {
                sum += val;
            }
            return sum;
        }
    }
}
