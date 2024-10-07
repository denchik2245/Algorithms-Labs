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
                steps++; // Увеличиваем количество шагов

                if (power % 2 == 1) // Нечетная степень
                {
                    result *= baseValue; // Умножаем результат на базу
                    power--; // Уменьшаем степень на 1
                }
                else
                {
                    baseValue *= baseValue; // Возводим базу в квадрат
                    power /= 2; // Делим степень на 2
                }
            }

            return result;
        }
    }
}
