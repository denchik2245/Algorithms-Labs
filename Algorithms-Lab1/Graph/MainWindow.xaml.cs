using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using MyLibrary.Logic.Vector;
using MyLibrary.Logic.Algorithms;
using MyLibrary.Logic.Operation;
using MyVectorLibrary.Sorters;
using MathNet.Numerics;

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
                    MaxElementsTextBlock.Text = "Основание степени";
                    StepIncrementTextBlock.Text = "Шаг увеличения степени";
                }
                else
                {
                    RunsTextBlock.Text = "Кол-во запусков";
                    MaxElementsTextBlock.Text = "Макс. кол-во элементов";
                    StepIncrementTextBlock.Text = "Шаг увеличения данных";
                }
            }
        }

        // Обработчик кнопки расчета
        private void ButtonСalculation_Click(object sender, RoutedEventArgs e)
        {
            if (AlgorithmComboBox.SelectedItem == null || 
                !int.TryParse(RunsTextBox.Text, out int runs) || runs <= 0 || 
                !int.TryParse(MaxElementsTextBox.Text, out int maxElements) || maxElements <= 0 || 
                !int.TryParse(StepIncrementTextBox.Text, out int stepIncrement) || stepIncrement <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректные данные.");
                return;
            }

            int[] sizes = Enumerable.Range(1, maxElements / stepIncrement).Select(x => x * stepIncrement).ToArray();
            double[] times = new double[sizes.Length];

            string selectedAlgorithm = AlgorithmComboBox.SelectedItem.ToString();
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

            PlotGraph(sizes, times);
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
        private void PlotGraph(int[] sizes, double[] times)
        {
            string selectedAlgorithm = AlgorithmComboBox.SelectedItem.ToString();
            
            AlgorithmTitleTextBlock.Text = $"Алгоритм: {selectedAlgorithm}";
            ApproximationTitleTextBlock.Text = "Апроксимация";

            LineSeries lineSeries = new LineSeries
            {
                Title = selectedAlgorithm,
                Values = new ChartValues<double>(times),
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 5
            };
            
            MyChart.Series.Clear();
            MyChart.Series.Add(lineSeries);

            // Апроксимация
            var coefficients = Fit.Polynomial(sizes.Select(x => (double)x).ToArray(), times, 2);
            var polynomial = new Func<double, double>(x => coefficients[0] + coefficients[1] * x + coefficients[2] * x * x);
            double[] approximatedValues = sizes.Select(x => polynomial(x)).ToArray();

            LineSeries approximationSeries = new LineSeries
            {
                Title = "Апроксимация",
                Values = new ChartValues<double>(approximatedValues),
                LineSmoothness = 0,
                StrokeDashArray = new System.Windows.Media.DoubleCollection { 2 },
                PointGeometry = null
            };

            MyChart.Series.Add(approximationSeries);
            
            MyChart.AxisX.Clear();
            MyChart.AxisX.Add(new Axis
            {
                Title = "Размер массива (кол-во элементов)",
                Labels = sizes.Select(x => x.ToString()).ToArray()
            });

            MyChart.AxisY.Clear();
            MyChart.AxisY.Add(new Axis
            {
                Title = "Время (мс)",
                LabelFormatter = value => value.ToString("F5")
            });
            
            MyChart.Zoom = ZoomingOptions.Xy;
            MyChart.AnimationsSpeed = TimeSpan.FromMilliseconds(200);
        }
    }
}
