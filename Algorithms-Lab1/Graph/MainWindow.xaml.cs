using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using Logic.Algorithms;
using MyLibrary.Logic.Vector;
using MyLibrary.Logic.Algorithms;
using MyLibrary.Logic.Operation;
using MyVectorLibrary.Sorters;
using MathNet.Numerics;
using MyLibrary.Logic.Matrix;

namespace WpfApp
{
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConfigureChart();
            AlgorithmTypeComboBox.SelectedIndex = 0;
            AlgorithmComboBox.SelectedIndex = 0;
        }

        // Настройка графика
        private void ConfigureChart()
        {
            MyChart.AxisX.Clear();
            MyChart.AxisX.Add(new Axis
            {
                Title = "Размер массива",
                LabelFormatter = value => value.ToString("F0")
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

        // Обработчик изменения типа алгоритма
        private void AlgorithmTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AlgorithmTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                AlgorithmComboBox.Items.Clear();
                var algorithms = selectedItem.Content.ToString() switch
                {
                    "Сортировка" => new[] { "Сортировка пузырьком", "Сортировка обменом", "Блинная сортировка", "Быстрая сортировка", "Гибридная сортировка", "Сортировка Шелла" },
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
                
                // Изменение названий полей ввода для алгоритмов возведения в степень
                if (selectedItem.Content.ToString() == "Возведение в степень")
                {
                    RunsTextBlock.Text = "Максимальная степень";
                    StepIncrementTextBlock.Text = "Шаг увеличения степени";

                    // Скрываем поле "Основание степени"
                    MaxElementsTextBlock.Visibility = Visibility.Collapsed;
                    MaxElementsTextBox.Visibility = Visibility.Collapsed;
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

        // Обработчик кнопки расчета
        private void ButtonСalculation_Click(object sender, RoutedEventArgs e)
        {
            if (AlgorithmComboBox.SelectedItem == null ||
                !int.TryParse(RunsTextBox.Text, out int runs) || runs <= 0 ||
                !int.TryParse(StepIncrementTextBox.Text, out int stepIncrement) || stepIncrement <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректные данные.");
                return;
            }

            string selectedAlgorithmType = (AlgorithmTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string selectedAlgorithm = AlgorithmComboBox.SelectedItem.ToString();

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
            else if (selectedAlgorithmType == "Возведение в степень")
            {
                // Генерация степеней от 1 до maxExponent с шагом stepIncrement
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
            else
            {
                // Для остальных алгоритмов (сортировка и т.д.)
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
                "Постоянная функция" => array => new ConstantFunction().Calculate(array),
                "Умножение элементов" => array => new MultiplicationOfElements().Calculate(array),
                "Сумма элементов" => array => new SumOfElements().Calculate(array),
                "Прямое вычисление" => array => new NaivePolynomialEvaluation().Calculate(array, 1.0),
                "Схема Горнера" => array => new HornerMethod().Calculate(array, 1.0),
                "Сортировка Шелла" => array => new ShellSort().Sort(array),
                _ => null
            };
        }

        // Построение графика
        private void PlotGraph(int[] xValues, double[] yValues, string yAxisTitle, string xAxisTitle)
        {
            string selectedAlgorithm = AlgorithmComboBox.SelectedItem.ToString();
            
            AlgorithmTitleTextBlock.Text = $"Алгоритм: {selectedAlgorithm}";
            
            LineSeries lineSeries = new LineSeries
            {
                Title = selectedAlgorithm,
                Values = new ChartValues<double>(yValues),
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 5
            };
            
            MyChart.Series.Clear();
            MyChart.Series.Add(lineSeries);

            // Добавляем апроксимацию только для тех алгоритмов, где ось Y — время выполнения
            if (yAxisTitle == "Время выполнения (мс)")
            {
                // Построение апроксимации
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
                // Если это возведение в степень, апроксимацию не строим
                ApproximationTitleTextBlock.Text = string.Empty;
            }

            // Настройка осей графика
            MyChart.AxisX.Clear();
            MyChart.AxisX.Add(new Axis
            {
                Title = xAxisTitle,
                Labels = xValues.Select(x => x.ToString()).ToArray()
            });

            MyChart.AxisY.Clear();
            MyChart.AxisY.Add(new Axis
            {
                Title = yAxisTitle,
                LabelFormatter = value => value.ToString("F5")
            });
            
            MyChart.Zoom = ZoomingOptions.Xy;
            MyChart.AnimationsSpeed = TimeSpan.FromMilliseconds(200);
        }
    }
}
