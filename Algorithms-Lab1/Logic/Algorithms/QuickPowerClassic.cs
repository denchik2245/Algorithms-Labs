using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Algorithms
{
    public class QuickPowerClassic : IPowerRaiser
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
            if (power == 0) return 1; // База рекурсии
            if ((power % 2) == 0) // Проверка на четность
            {
                int result = CalculatePower(num, power / 2); // Делим степень на 2
                return result * result; // Возвращаем квадрат
            }
            else
            {
                return num * CalculatePower(num, power - 1); // Нечетная степень
            }
        }
    }
}
