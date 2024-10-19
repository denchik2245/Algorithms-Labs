namespace Algorithms_Lab1.Logic.Algorithms
{
    public class QuickSort : ISorter
    {
        public void Sort(int[] arr)
        {
            if (arr == null)
                throw new ArgumentNullException(nameof(arr));

            QuickSortHelper(arr, 0, arr.Length - 1);
        }

        private void QuickSortHelper(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(arr, low, high);
                QuickSortHelper(arr, low, pivotIndex - 1);
                QuickSortHelper(arr, pivotIndex + 1, high);
            }
        }

        private int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }

            (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
            return i + 1;
        }
    }
}