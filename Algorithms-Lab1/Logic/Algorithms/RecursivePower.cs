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
                array[i] = Power(array[i], power);
            }
        }

        static int Power(int baseValue, int power)
        {
            // Базовый случай
            if (power == 0)
                return 1;

            // Рекурсивный случай
            return baseValue * Power(baseValue, power - 1);
        }
    }
}
