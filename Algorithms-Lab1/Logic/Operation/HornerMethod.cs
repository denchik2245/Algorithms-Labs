namespace MyLibrary.Logic.Operation
{
    public class HornerMethod
    {
        public double Calculate(int[] coefficients, double x)
        {
            int n = coefficients.Length;
            double result = coefficients[n - 1];
            for (int k = n - 2; k >= 0; k--)
            {
                result = result * x + coefficients[k];
            }
            return result;
        }
    }
}