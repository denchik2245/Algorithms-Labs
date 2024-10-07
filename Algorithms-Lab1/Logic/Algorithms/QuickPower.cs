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
                int steps = 0;  // Явно объявляем переменную steps
                array[i] = CalculatePower(array[i], power, ref steps);
            }
        }

        public static int CalculatePower(int num, int power, ref int steps)
        {
            steps = 0;
            int result = 1;

            while (power > 0)
            {
                steps++; // Увеличиваем шаг

                if ((power % 2) == 1)
                {
                    result *= num;
                }

                power /= 2;
                num *= num;
            }
            return result;
        }
    }

}
