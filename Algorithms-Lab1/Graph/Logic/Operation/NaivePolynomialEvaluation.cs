namespace Algorithms_Lab1.Logic.Operation
{
    public class NaivePolynomialEvaluation
    {
        public double Calculate(int[] coefficients, double x)
        {
            int n = coefficients.Length;
            double result = 0;
            double currentPower = 1;
            
            for (int k = 0; k < n; k++)
            {
                result += coefficients[k] * currentPower;
                currentPower *= x;
            }
            return result;
        }
    }
}