using MyVectorLibrary.Sorters;

namespace MyLibrary.Logic.Algorithms
{
    public class CombSort : ISorter
    {
        public void Sort(int[] array)
        {
            int n = array.Length;
            int gap = n;
            bool swapped = true;

            while (gap > 1 || swapped)
            {
                if (gap > 1)
                {
                    gap = GetNextGap(gap);
                }

                swapped = false;

                for (int i = 0; i + gap < n; i++)
                {
                    if (array[i] > array[i + gap])
                    {
                        Swap(ref array[i], ref array[i + gap]);
                        swapped = true;
                    }
                }
            }
        }

        private int GetNextGap(int gap)
        {
            gap = gap * 10 / 13;
            return gap < 1 ? 1 : gap;
        }

        private void Swap(ref int a, ref int b)
        {
            (a, b) = (b, a);
        }
    }
}