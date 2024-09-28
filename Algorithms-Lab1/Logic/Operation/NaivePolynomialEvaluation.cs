namespace MyLibrary.Logic.Operation
{
    public class NaivePolynomialEvaluation
    {
        public double Calculate(int[] coefficients, double x)
        {
            int n = coefficients.Length;
            double result = 0;
            for (int k = 0; k < n; k++)
            {
                result += coefficients[k] * Math.Pow(x, k);
            }
            return result;
        }
    }
}