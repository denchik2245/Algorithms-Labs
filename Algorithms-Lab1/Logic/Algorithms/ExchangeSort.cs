using MyVectorLibrary.Sorters;

namespace MyLibrary.Logic.Algorithms
{
    public class ExchangeSort : ISorter
    {
        public void Sort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (array[i] > array[j])
                    {
                        int temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }
    }
}