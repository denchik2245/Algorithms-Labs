using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Algorithms
{
    public class BinaryPower : IPowerRaiser
    {
        public void RaiseToPower(int[] array, int power)
        {
            if (power < 0) throw new ArgumentException("Степень должна быть неотрицательным числом");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = BinPower(array[i], power);
            }
        }

        private static int BinPower(int num, int power)
        {
            int result = 1;
            while (power > 0)
            {
                if ((power & 1) == 1)
                { // Если power нечётное
                    result *= num;
                }
                num *= num; // Возводим num в квадрат
                power >>= 1; // Делим power на 2
            }
            return result;
        }
    }
}
