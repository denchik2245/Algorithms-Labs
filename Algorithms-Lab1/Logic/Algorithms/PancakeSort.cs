using MyVectorLibrary.Sorters;

namespace MyLibrary.Logic.Algorithms
{
    public class PancakeSort : ISorter
    {
        public void Sort(int[] array)
        {
            for (int currIndex = array.Length - 1; currIndex > 0; currIndex--)
            {
                int maxValueIndex = FindMaxValueIndex(array, currIndex);

                if (maxValueIndex != currIndex)
                {
                    Flip(array, maxValueIndex);
                    Flip(array, currIndex);
                }
            }
        }

        private static int FindMaxValueIndex(int[] array, int index)
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
                (array[start], array[index]) = (array[index], array[start]);
                index--;
                start++;
            }
        }
    }
}
