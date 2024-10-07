using MyVectorLibrary.Sorters;

namespace MyLibrary.Logic.Algorithms
{
    public class ShellSort : ISorter
    {
        public void Sort(int[] arr)
        {
            int n = arr.Length;

            // Используем последовательность интервалов по Шеллу: n/2, n/4, ..., 1
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                // Проходим по элементам, начиная с индекса gap
                for (int i = gap; i < n; i++)
                {
                    int temp = arr[i];
                    int j;

                    // Перемещаем элементы arr[0..i-gap], которые больше temp, на позицию вперед на расстояние gap
                    for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                    {
                        arr[j] = arr[j - gap];
                    }

                    // Вставляем temp на правильное место
                    arr[j] = temp;
                }
            }
        }
    }
}