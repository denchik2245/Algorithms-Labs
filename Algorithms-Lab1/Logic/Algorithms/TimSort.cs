using MyVectorLibrary.Sorters;

namespace MyLibrary.Logic.Algorithms
{
    public class Timsort : ISorter
    {
        private const int RUN = 32;

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
        }
        
        private void InsertionSort(int[] array, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                int temp = array[i];
                int j = i - 1;

                while (j >= left && array[j] > temp)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = temp;
            }
        }
        
        private void Merge(int[] array, int left, int mid, int right)
        {
            int len1 = mid - left + 1, len2 = right - mid;
            int[] leftArray = new int[len1];
            int[] rightArray = new int[len2];

            for (int i = 0; i < len1; i++)
                leftArray[i] = array[left + i];
            for (int i = 0; i < len2; i++)
                rightArray[i] = array[mid + 1 + i];

            int iLeft = 0, iRight = 0, k = left;

            while (iLeft < len1 && iRight < len2)
            {
                if (leftArray[iLeft] <= rightArray[iRight])
                {
                    array[k] = leftArray[iLeft];
                    iLeft++;
                }
                else
                {
                    array[k] = rightArray[iRight];
                    iRight++;
                }
                k++;
            }

            while (iLeft < len1)
            {
                array[k] = leftArray[iLeft];
                iLeft++;
                k++;
            }

            while (iRight < len2)
            {
                array[k] = rightArray[iRight];
                iRight++;
                k++;
            }
        }
    }
}
