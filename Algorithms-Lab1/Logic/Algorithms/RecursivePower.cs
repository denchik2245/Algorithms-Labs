using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Algorithms
{
    public class RecursivePower : IPowerRaiser
    {
        public void RaiseToPower(int[] array, int power)
        {
            if (power < 0) throw new ArgumentException("Степень должна быть неотрицательным числом");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = CalculatePower(array[i], power);
            }
        }

        private static int CalculatePower(int baseValue, int power)
        {
            if (power == 0)
                return 1;
            int result = CalculatePower(baseValue, power / 2);
            if (power % 2 == 1)
                result = result * result * baseValue;
            else
                result = result * baseValue;
            return result;
        }
    }
}
