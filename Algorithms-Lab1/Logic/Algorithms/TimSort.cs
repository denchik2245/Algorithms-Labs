using MyVectorLibrary.Sorters;

namespace MyLibrary.Logic.Algorithms
{
    public class Run
    {
        public int StartIndex { get; set; }
        public int Length { get; set; }
    }

    public class TimSort : ISorter
    {
        private const int MIN_MERGE = 32;

        public void Sort(int[] array)
        {
            int n = array.Length;
            int minRun = GetMinrun(n);
            Stack<Run> runStack = new Stack<Run>();

            int i = 0;
            while (i < n)
            {
                int runStart = i;
                int runLength = 1;
                
                if (i < n - 1 && array[i] <= array[i + 1])
                {
                    while (i < n - 1 && array[i] <= array[i + 1])
                    {
                        i++;
                        runLength++;
                    }
                }
                else
                {
                    while (i < n - 1 && array[i] > array[i + 1])
                    {
                        i++;
                        runLength++;
                    }
                    Array.Reverse(array, runStart, runLength);
                }
                
                if (runLength < minRun)
                {
                    int rightLimit = Math.Min(runStart + minRun - 1, n - 1);
                    InsertionSort(array, runStart, rightLimit);
                    runLength = rightLimit - runStart + 1;
                }
                
                Run run = new Run { StartIndex = runStart, Length = runLength };
                StackPush(array, runStack, run);

                i = runStart + runLength;
            }
            
            while (runStack.Count > 1)
            {
                Run A = runStack.Pop();
                Run B = runStack.Pop();

                MergeRuns(array, runStack, B, A);
            }
        }

        private static int GetMinrun(int n)
        {
            int r = 0;
            while (n >= MIN_MERGE)
            {
                r |= n & 1;
                n >>= 1;
            }
            return n + r;
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
            int len1 = mid - left + 1;
            int len2 = right - mid;

            int[] leftArray = new int[len1];
            int[] rightArray = new int[len2];

            Array.Copy(array, left, leftArray, 0, len1);
            Array.Copy(array, mid + 1, rightArray, 0, len2);

            int iLeft = 0, iRight = 0, iArr = left;

            int gallopCount = 7;
            int countLeft = 0;
            int countRight = 0;

            while (iLeft < len1 && iRight < len2)
            {
                if (leftArray[iLeft] <= rightArray[iRight])
                {
                    array[iArr++] = leftArray[iLeft++];
                    countLeft++;
                    countRight = 0;
                }
                else
                {
                    array[iArr++] = rightArray[iRight++];
                    countRight++;
                    countLeft = 0;
                }

                if (countLeft >= gallopCount)
                {
                    int newILeft = GallopRight(rightArray[iRight], leftArray, iLeft, len1 - iLeft);
                    int numCopied = newILeft - iLeft;
                    Array.Copy(leftArray, iLeft, array, iArr, numCopied);
                    iArr += numCopied;
                    iLeft = newILeft;
                    countLeft = 0;
                }
                else if (countRight >= gallopCount)
                {
                    int newIRight = GallopLeft(leftArray[iLeft], rightArray, iRight, len2 - iRight);
                    int numCopied = newIRight - iRight;
                    Array.Copy(rightArray, iRight, array, iArr, numCopied);
                    iArr += numCopied;
                    iRight = newIRight;
                    countRight = 0;
                }
            }
            
            while (iLeft < len1)
            {
                array[iArr++] = leftArray[iLeft++];
            }

            while (iRight < len2)
            {
                array[iArr++] = rightArray[iRight++];
            }
        }

        private int GallopLeft(int key, int[] array, int start, int length)
        {
            int low = start;
            int high = start + length;

            while (low < high)
            {
                int mid = low + (high - low) / 2;
                if (key <= array[mid])
                {
                    high = mid;
                }
                else
                {
                    low = mid + 1;
                }
            }
            return low;
        }

        private int GallopRight(int key, int[] array, int start, int length)
        {
            int low = start;
            int high = start + length;

            while (low < high)
            {
                int mid = low + (high - low) / 2;
                if (key < array[mid])
                {
                    high = mid;
                }
                else
                {
                    low = mid + 1;
                }
            }
            return low;
        }


        private void StackPush(int[] array, Stack<Run> stack, Run run)
        {
            stack.Push(run);

            while (stack.Count > 1)
            {
                Run A = stack.Pop();
                Run B = stack.Pop();
                
                if (stack.Count > 0)
                {
                    Run C = stack.Peek();

                    if (C.Length <= B.Length + A.Length)
                    {
                        if (C.Length < A.Length)
                        {
                            MergeRuns(array, stack, C, B);
                        }
                        else
                        {
                            MergeRuns(array, stack, B, A);
                        }
                    }
                    else if (B.Length <= A.Length)
                    {
                        MergeRuns(array, stack, B, A);
                    }
                    else
                    {
                        stack.Push(B);
                        stack.Push(A);
                        break;
                    }
                }
                else
                {
                    if (B.Length <= A.Length)
                    {
                        MergeRuns(array, stack, B, A);
                    }
                    else
                    {
                        stack.Push(B);
                        stack.Push(A);
                        break;
                    }
                }
            }
        }

        private void MergeRuns(int[] array, Stack<Run> stack, Run leftRun, Run rightRun)
        {
            Merge(array, leftRun.StartIndex, leftRun.StartIndex + leftRun.Length - 1, rightRun.StartIndex + rightRun.Length - 1);
            Run mergedRun = new Run
            {
                StartIndex = leftRun.StartIndex,
                Length = leftRun.Length + rightRun.Length
            };
            if (stack.Count > 0)
            {
                stack.Pop();
            }
            stack.Push(mergedRun);
        }
    }
}
