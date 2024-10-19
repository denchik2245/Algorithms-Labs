namespace Algorithms_Lab1.Logic.Algorithms
{
    public class BubbleSort : ISorter
    {
        public void Sort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    { 
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }
        }
    }
}

