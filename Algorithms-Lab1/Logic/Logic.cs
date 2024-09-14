namespace MyVectorLibrary
{
    public class Logic
    {
        private static Random rand = new Random();

        //Cоздает вектор случайных неотрицательных чисел размером n
        public static int[] GenerateRandomVector(int n)
        {
            if (n <= 0)
                throw new ArgumentException("Размер вектора должен быть положительным числом.");

            int[] v = new int[n];

            for (int i = 0; i < n; i++)
            {
                v[i] = rand.Next(1, 101); // Генерирует целые числа от 1 до 100
            }

            return v;
        }

        //Возвращает фиксированное значение
        public static int ConstantFunction()
        {
            return 1;
        }

        //Вычисляет сумму элементов вектора v
        public static int SumElements(int[] v)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            int sum = 0;
            foreach (int value in v)
            {
                sum += value;
            }
            return sum;
        }

        //Вычисляет произведение элементов вектора v
        public static long ProductElements(int[] v)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            long product = 1;
            foreach (int value in v)
            {
                product *= value;
            }
            return product;
        }

        //Вычисляет значение многочлена прямым методом для заданного x
        public static double EvaluatePolynomialDirect(int[] v, double x)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            int n = v.Length;
            double result = 0.0;

            for (int i = 0; i < n; i++)
            {
                result += v[i] * Math.Pow(x, n - i - 1);
            }

            return result;
        }

        //Вычисляет значение многочлена методом Горнера для заданного x
        public static double EvaluatePolynomialHorner(int[] v, double x)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            double result = v[0];

            for (int i = 1; i < v.Length; i++)
            {
                result = result * x + v[i];
            }

            return result;
        }

        //Пузырьковая сортировка
        public static void BubbleSort(int[] v)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            int n = v.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (v[j] > v[j + 1])
                    {
                        (v[j], v[j + 1]) = (v[j + 1], v[j]);
                    }
                }
            }
        }

        //Быстрая сортировка
        public static void QuickSort(int[] v)
        {
            if (v == null)
                throw new ArgumentNullException(nameof(v));

            QuickSortHelper(v, 0, v.Length - 1);
        }

        private static void QuickSortHelper(int[] v, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(v, low, high);
                QuickSortHelper(v, low, pivotIndex - 1);
                QuickSortHelper(v, pivotIndex + 1, high);
            }
        }

        private static int Partition(int[] v, int low, int high)
        {
            int pivot = v[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (v[j] <= pivot)
                {
                    i++;
                    (v[i], v[j]) = (v[j], v[i]);
                }
            }

            (v[i + 1], v[high]) = (v[high], v[i + 1]);

            return i + 1;
        }
        
        //Гибридная сортировка
        private const int RUN = 32;
        public static void TimSort(int[] arr)
        {
            int n = arr.Length;
            
            for (int i = 0; i < n; i += RUN)
            {
                InsertionSort(arr, i, Math.Min((i + RUN - 1), (n - 1)));
            }
            
            for (int size = RUN; size < n; size = 2 * size)
            {
                for (int left = 0; left < n; left += 2 * size)
                {
                    int mid = left + size - 1;
                    int right = Math.Min((left + 2 * size - 1), (n - 1));

                    if (mid < right)
                        Merge(arr, left, mid, right);
                }
            }
        }
        
        private static void InsertionSort(int[] arr, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                int temp = arr[i];
                int j = i - 1;
                while (j >= left && arr[j] > temp)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = temp;
            }
        }
        
        private static void Merge(int[] arr, int l, int m, int r)
        {
            int len1 = m - l + 1, len2 = r - m;
            int[] left = new int[len1];
            int[] right = new int[len2];

            for (int x = 0; x < len1; x++)
                left[x] = arr[l + x];
            for (int x = 0; x < len2; x++)
                right[x] = arr[m + 1 + x];

            int i = 0, j = 0, k = l;

            while (i < len1 && j < len2)
            {
                if (left[i] <= right[j])
                {
                    arr[k++] = left[i++];
                }
                else
                {
                    arr[k++] = right[j++];
                }
            }

            while (i < len1)
            {
                arr[k++] = left[i++];
            }

            while (j < len2)
            {
                arr[k++] = right[j++];
            }
        }
        
    }
}
