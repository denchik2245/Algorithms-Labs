using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Algorithms_Lab1.Logic.Algorithms;
using Algorithms_Lab1.Logic.Matrix;
using Algorithms_Lab1.Logic.Operation;
using Algorithms_Lab1.Logic.Vector;
using LiveCharts;
using LiveCharts.Wpf;
using MathNet.Numerics;

namespace Algorithms_Lab1
{
    public partial class MainWindow : System.Windows.Window
    {
        //Инициализируем все элементы интерфейса
        public MainWindow()
        {
            InitializeComponent();
            ConfigureChart();
            AlgorithmTypeComboBox.SelectedIndex = 0;
            AlgorithmComboBox.SelectedIndex = 0;
        }
        
        //Настраиваем оси и параметры графика
        private void ConfigureChart()
        {
            MyChart.AxisX.Clear();
            MyChart.AxisX.Add(new Axis
            {
                Title = "Размер массива",
                LabelFormatter = value => value.ToString("F0"),
                MinValue = 0
            });

            MyChart.AxisY.Clear();
            MyChart.AxisY.Add(new Axis
            {
                Title = "Время выполнения (мс)",
                LabelFormatter = value => value.ToString("F0")
            });

            MyChart.AnimationsSpeed = TimeSpan.FromMilliseconds(300);
            MyChart.Zoom = ZoomingOptions.Xy;
            MyChart.LegendLocation = LegendLocation.None;
        }
        
        //Обновляем список доступных алгоритмов в зависимости от выбранного типа
        private void AlgorithmTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AlgorithmTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                AlgorithmComboBox.Items.Clear();
                var algorithms = selectedItem.Content.ToString() switch
                {
                    "Сортировка" => new[] { "Сортировка пузырьком", "Сортировка обменом", "Блинная сортировка", "Быстрая сортировка", "Гибридная сортировка", "Сортировка Шелла", "Сортировка расчёской" },
                    "Математические операции" => new[] { "Постоянная функция", "Умножение элементов", "Сумма элементов" },
                    "Матричные операции" => new[] { "Умножение матриц" },
                    "Возведение в степень" => new[] { "Простое возведение", "Рекурсивное возведение", "Быстрое возведение", "Классическое быстрое возведение" },
                    "Полиномы" => new[] { "Прямое вычисление", "Схема Горнера" },
                    _ => Array.Empty<string>()
                };

                foreach (var algorithm in algorithms)
                {
                    AlgorithmComboBox.Items.Add(algorithm);
                }
                
                if (AlgorithmComboBox.Items.Count > 0)
                {
                    AlgorithmComboBox.SelectedIndex = 0;
                }
                
                if (selectedItem.Content.ToString() == "Возведение в степень")
                {
                    RunsTextBlock.Text = "Максимальная степень";
                    StepIncrementTextBlock.Text = "Шаг увеличения степени";
                    
                    MaxElementsTextBlock.Visibility = Visibility.Collapsed;
                    MaxElementsTextBox.Visibility = Visibility.Collapsed;
                    
                    RunsTextBox.Text = "100";
                    StepIncrementTextBox.Text = "1";
                }
                else
                {
                    RunsTextBlock.Text = "Кол-во запусков";
                    StepIncrementTextBlock.Text = "Шаг увеличения данных";

                    MaxElementsTextBlock.Visibility = Visibility.Visible;
                    MaxElementsTextBox.Visibility = Visibility.Visible;
                }
            }
        }
        
        //Обработчик нажатия кнопки
        private void ButtonСalculation_Click(object sender, RoutedEventArgs e)
        {
            //Проверка введенных данных
            if (AlgorithmComboBox.SelectedItem == null ||
                !int.TryParse(RunsTextBox.Text, out int runs) || runs <= 0 ||
                !int.TryParse(StepIncrementTextBox.Text, out int stepIncrement) || stepIncrement <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректные данные.");
                return;
            }

            //Определение выбранного алгоритма
            string selectedAlgorithmType = (AlgorithmTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string selectedAlgorithm = AlgorithmComboBox.SelectedItem.ToString();

            //Выполнение матричных операций
            if (selectedAlgorithmType == "Матричные операции" && selectedAlgorithm == "Умножение матриц")
            {
                if (!int.TryParse(MaxElementsTextBox.Text, out int maxSize) || maxSize <= 0)
                {
                    MessageBox.Show("Пожалуйста, введите корректные данные.");
                    return;
                }

                int[] sizes = Enumerable.Range(1, maxSize / stepIncrement).Select(x => x * stepIncrement).ToArray();
                double[] times = new double[sizes.Length];

                for (int i = 0; i < sizes.Length; i++)
                {
                    int size = sizes[i];
                    double totalTime = 0;

                    for (int run = 0; run < runs; run++)
                    {
                        int[,] matrixA = MatrixGenerator.GenerateRandomSquareMatrix(size, 0, 10);
                        int[,] matrixB = MatrixGenerator.GenerateRandomSquareMatrix(size, 0, 10);

                        Stopwatch stopwatch = Stopwatch.StartNew();
                        MatrixGenerator.OptimizedMultiplyMatrices(matrixA, matrixB);
                        stopwatch.Stop();

                        totalTime += stopwatch.Elapsed.TotalMilliseconds;
                    }

                    times[i] = totalTime / runs;
                }

                PlotGraph(sizes, times, "Время выполнения (мс)", "Размер матрицы (N x N)");
            }
            
            //Возведение в степень
            else if (selectedAlgorithmType == "Возведение в степень")
            {
                if (!int.TryParse(RunsTextBox.Text, out int maxExponent) || maxExponent <= 0)
                {
                    MessageBox.Show("Пожалуйста, введите корректные данные.");
                    return;
                }

                int[] exponents = Enumerable.Range(1, maxExponent / stepIncrement).Select(i => i * stepIncrement).ToArray();
                double[] steps = new double[exponents.Length];

                Func<int[], int, int> powerAlgorithmWithSteps = GetPowerAlgorithmActionWithSteps(selectedAlgorithm);

                if (powerAlgorithmWithSteps == null)
                {
                    MessageBox.Show("Данный алгоритм еще не реализован.");
                    return;
                }

                for (int i = 0; i < exponents.Length; i++)
                {
                    int exponent = exponents[i];
                    int[] array = VectorGenerator.GenerateRandomVector(1);
                    steps[i] = powerAlgorithmWithSteps(array, exponent);
                }

                PlotGraph(exponents, steps, "Количество шагов", "Степень");
            }
            
            //Другие алгоритмы
            else
            {
                if (!int.TryParse(MaxElementsTextBox.Text, out int maxElements) || maxElements <= 0)
                {
                    MessageBox.Show("Пожалуйста, введите корректные данные.");
                    return;
                }

                int[] sizes = Enumerable.Range(1, maxElements / stepIncrement).Select(x => x * stepIncrement).ToArray();
                double[] times = new double[sizes.Length];

                Action<int[]> algorithmAction = GetAlgorithmAction(selectedAlgorithm);

                if (algorithmAction == null)
                {
                    MessageBox.Show("Данный алгоритм еще не реализован.");
                    return;
                }

                for (int i = 0; i < sizes.Length; i++)
                {
                    int size = sizes[i];
                    double totalTime = 0;

                    for (int run = 0; run < runs; run++)
                    {
                        int[] array = VectorGenerator.GenerateRandomVector(size);
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        algorithmAction(array);
                        stopwatch.Stop();

                        totalTime += stopwatch.Elapsed.TotalMilliseconds;
                    }

                    times[i] = totalTime / runs;
                }

                PlotGraph(sizes, times, "Время выполнения (мс)", "Размер массива (кол-во элементов)");
            }
        }


        private Func<int[], int, int> GetPowerAlgorithmActionWithSteps(string selectedAlgorithm)
        {
            return selectedAlgorithm switch
            {
                "Простое возведение" => (array, power) =>
                {
                    return power;
                },
                "Рекурсивное возведение" => (array, power) =>
                {
                    int steps;
                    new RecursivePower().RaiseToPower(array, power);
                    RecursivePower.Power(array[0], power, out steps);
                    return steps;
                },
                "Быстрое возведение" => (array, power) =>
                {
                    int steps = 0;
                    new QuickPower().RaiseToPower(array, power);
                    QuickPower.CalculatePower(array[0], power, ref steps);
                    return steps;
                },
                "Классическое быстрое возведение" => (array, power) =>
                {
                    int steps = 0;
                    new QuickPowerClassic().RaiseToPower(array, power);
                    QuickPowerClassic.CalculatePowerWithSteps(array[0], power, ref steps);
                    return steps;
                },
                _ => null
            };
        }
        
        // Определение действия для выбранного алгоритма
        private Action<int[]> GetAlgorithmAction(string selectedAlgorithm)
        {
            return selectedAlgorithm switch
            {
                "Быстрая сортировка" => array => new QuickSort().Sort(array),
                "Сортировка пузырьком" => array => new BubbleSort().Sort(array),
                "Сортировка обменом" => array => new ExchangeSort().Sort(array),
                "Блинная сортировка" => array => new PancakeSort().Sort(array),
                "Гибридная сортировка" => array => new TimSort().Sort(array),
                "Сортировка Шелла" => array => new ShellSort().Sort(array),
                "Сортировка расчёской" => array => new CombSort().Sort(array),
                "Постоянная функция" => array => new ConstantFunction().Calculate(array),
                "Умножение элементов" => array => new MultiplicationOfElements().Calculate(array),
                "Сумма элементов" => array => new SumOfElements().Calculate(array),
                "Прямое вычисление" => array => new NaivePolynomialEvaluation().Calculate(array, 1.0),
                "Схема Горнера" => array => new HornerMethod().Calculate(array, 1.0),
                _ => null
            };
        }

        // Построение графика
        private void PlotGraph(int[] xValues, double[] yValues, string yAxisTitle, string xAxisTitle)
        {
            string selectedAlgorithm = AlgorithmComboBox.SelectedItem.ToString();
            
            AlgorithmTitleTextBlock.Text = $"Алгоритм: {selectedAlgorithm}";
            
            // Добавляем 0 в начало массива xValues, чтобы график всегда начинался с нуля
            var updatedXValues = new int[xValues.Length + 1];
            updatedXValues[0] = 0;
            Array.Copy(xValues, 0, updatedXValues, 1, xValues.Length);

            // Для yValues также добавляем 0 для согласованности
            var updatedYValues = new double[yValues.Length + 1];
            updatedYValues[0] = 0; // Соответствующее значение для оси Y в точке 0
            Array.Copy(yValues, 0, updatedYValues, 1, yValues.Length);
            
            LineSeries lineSeries = new LineSeries
            {
                Title = selectedAlgorithm,
                Values = new ChartValues<double>(yValues),
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 5
            };
            
            MyChart.Series.Clear();
            MyChart.Series.Add(lineSeries);
            
            if (yAxisTitle == "Время выполнения (мс)")
            {
                ApproximationTitleTextBlock.Text = "Апроксимация";

                var coefficients = Fit.Polynomial(xValues.Select(x => (double)x).ToArray(), yValues, 2);
                var polynomial = new Func<double, double>(x => coefficients[0] + coefficients[1] * x + coefficients[2] * x * x);
                double[] approximatedValues = xValues.Select(x => polynomial(x)).ToArray();

                LineSeries approximationSeries = new LineSeries
                {
                    Title = "Апроксимация",
                    Values = new ChartValues<double>(approximatedValues),
                    LineSmoothness = 0,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection { 2 },
                    PointGeometry = null
                };

                MyChart.Series.Add(approximationSeries);
            }
            else
            {
                ApproximationTitleTextBlock.Text = string.Empty;
            }
            
            // Настройка оси X с началом от 0
            MyChart.AxisX.Clear();
            MyChart.AxisX.Add(new Axis
            {
                Title = xAxisTitle,
                Labels = updatedXValues.Select(x => x.ToString()).ToArray(),
                MinValue = 0 // Устанавливаем минимальное значение на оси X
            });

            // Настройка оси Y с началом от 0
            MyChart.AxisY.Clear();
            MyChart.AxisY.Add(new Axis
            {
                Title = yAxisTitle,
                LabelFormatter = value => value.ToString("F5"),
                MinValue = 0 // Устанавливаем минимальное значение на оси Y
            });
            
            MyChart.Zoom = ZoomingOptions.Xy;
            MyChart.AnimationsSpeed = TimeSpan.FromMilliseconds(200);
        }
    }
}
