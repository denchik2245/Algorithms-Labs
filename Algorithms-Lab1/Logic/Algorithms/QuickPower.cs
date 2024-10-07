using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Algorithms
{
    public class QuickPower : IPowerRaiser
    {
        public void RaiseToPower(int[] array, int power)
        {
            if (power < 0) throw new ArgumentException("Степень должна быть неотрицательным числом");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = CalculatePower(array[i], power);
            }
        }

        private static int CalculatePower(int num, int power)
        {
            int result = 1;
            if ((power % 2) == 1)
                result = num;
            while (power > 0)
            {
                power /= 2; // Делим power на 2
                num *= num; // Возводим num в квадрат
                if ((power %2) == 1)
                { // Если power нечётное
                    result *= num;
                }
            }
            return result;
        }
    }
}
