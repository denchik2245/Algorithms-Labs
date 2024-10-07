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
                array[i] = Power(array[i], power, out _);  // Выполняем возведение без учета шагов здесь
            }
        }

        public static int Power(int baseValue, int power, out int steps)
        {
            steps = 0;  // Инициализируем счетчик шагов

            if (power == 0)
                return 1;

            steps++; // Каждый вызов рекурсии - это шаг

            if (power % 2 == 0)
            {
                // Если степень четная, разбиваем пополам
                int halfPower = Power(baseValue, power / 2, out int recursiveSteps);
                steps += recursiveSteps; // Увеличиваем шаги на каждый рекурсивный вызов
                return halfPower * halfPower;
            }
            else
            {
                // Если степень нечетная, вычитаем 1 и продолжаем
                int reducedPower = Power(baseValue, power - 1, out int recursiveSteps);
                steps += recursiveSteps; // Увеличиваем шаги на каждый рекурсивный вызов
                return baseValue * reducedPower;
            }
        }
    }
}
