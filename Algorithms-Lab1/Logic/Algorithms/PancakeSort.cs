using MyVectorLibrary.Sorters;

namespace MyLibrary.Logic.Algorithms
{
    public class Timsort : ISorter
    {
        public void Sort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i += RUN)
            {
                InsertionSort(array, i, Math.Min((i + 31), (n - 1)));
            }
            
            for (int size = RUN; size < n; size = 2 * size)
            {
                for (int left = 0; left < n; left += 2 * size)
                {
                    int mid = left + size - 1;
                    int right = Math.Min((left + 2 * size - 1), (n - 1));
                    
                    if (mid < right)
                    {
                        Merge(array, left, mid, right);
                    }
                }
            }
        
        private void InsertionSort(int[] array, int left, int right)
        {
            int maxIndex = 0;
            for (int i = 1; i <= index; i++)
            {
                if (array[i] > array[maxIndex])
                {
                    maxIndex = i;
                }
            }
            return maxIndex;
        }
        
        private static void Flip(int[] array, int index)
                {
            int start = 0;
            while (start < index)
            {
                // Swap elements
                int temp = array[start];
                array[start] = array[index];
                array[index] = temp;

                index--;
                start++;
            }
        }
    }
}
