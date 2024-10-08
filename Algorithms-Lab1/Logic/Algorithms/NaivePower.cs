namespace Logic.Algorithms
{
    public class NaivePower : IPowerRaiser
    {
        public void RaiseToPower(int[] array, int power)
        {
            if (power < 0) throw new ArgumentException("Степень должна быть неотрицательным числом");

            for (int i = 0; i < array.Length; i++)
            {
                int result = 1;
                int baseValue = array[i];
                for (int j = 0; j < power; j++)
                {
                    result *= baseValue;
                }
                array[i] = result;
            }
        }
    }
}
