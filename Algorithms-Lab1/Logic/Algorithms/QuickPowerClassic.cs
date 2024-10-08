namespace Logic.Algorithms
{
    public class QuickPowerClassic : IPowerRaiser
    {
        public void RaiseToPower(int[] array, int power)
        {
            if (power < 0) throw new ArgumentException("Степень должна быть неотрицательным числом");

            int totalSteps = 0;
            for (int i = 0; i < array.Length; i++)
            {
                int steps = 0;
                array[i] = CalculatePowerWithSteps(array[i], power, ref steps);
                totalSteps += steps;
            }
        }

        public static int CalculatePowerWithSteps(int num, int power, ref int steps)
        {
            int result = 1;
            int baseValue = num;

            while (power > 0)
            {
                steps++;

                if (power % 2 == 1)
                {
                    result *= baseValue;
                    power--;
                }
                else
                {
                    baseValue *= baseValue;
                    power /= 2;
                }
            }

            return result;
        }
    }
}
