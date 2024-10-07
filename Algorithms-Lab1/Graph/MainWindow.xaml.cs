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
using LiveCharts.Defaults;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace WpfApp
{
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void AlgorithmTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AlgorithmTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                AlgorithmComboBox.Items.Clear();
                switch (selectedItem.Content.ToString())
                {
                    case "Сортировка":
                        AlgorithmComboBox.Items.Add("Сортировка пузырьком");
                        AlgorithmComboBox.Items.Add("Сортировка обменом");
                        AlgorithmComboBox.Items.Add("Блинная сортировка");
                        AlgorithmComboBox.Items.Add("Быстрая сортировка");
                        AlgorithmComboBox.Items.Add("Гибридная сортировка");
                        break;
                    case "Математические операции":
                        AlgorithmComboBox.Items.Add("Постоянная функция");
                        AlgorithmComboBox.Items.Add("Умножение элементов");
                        AlgorithmComboBox.Items.Add("Сумма элементов");
                        break;
                    case "Матричные операции":
                        AlgorithmComboBox.Items.Add("Умножение матриц");
                        break;
                    case "Возведение в степень":
                        AlgorithmComboBox.Items.Add("Простое возведение");
                        AlgorithmComboBox.Items.Add("Рекурсивное возведение");
                        AlgorithmComboBox.Items.Add("Быстрое возведение");
                        AlgorithmComboBox.Items.Add("Классическое быстрое возведение");
                        break;
                    case "Полиномы":
                        AlgorithmComboBox.Items.Add("Прямое вычисление");
                        AlgorithmComboBox.Items.Add("Схема Горнера");
                        break;
                }
            }
        }

        private void ButtonСalculation_Click(object sender, RoutedEventArgs e)
        {
            if (AlgorithmComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите алгоритм.");
                return;
            }

            if (!int.TryParse(RunsTextBox.Text, out int runs) || runs <= 0)
            {
                MessageBox.Show("Введите корректное количество запусков.");
                return;
            }

            if (!int.TryParse(MaxElementsTextBox.Text, out int maxElements) || maxElements <= 0)
            {
                MessageBox.Show("Введите корректное максимальное количество элементов.");
                return;
            }

            if (!int.TryParse(StepIncrementTextBox.Text, out int stepIncrement) || stepIncrement <= 0)
            {
                MessageBox.Show("Введите корректный шаг увеличения данных.");
                return;
            }

            int[] sizes = Enumerable.Range(1, maxElements / stepIncrement).Select(x => x * stepIncrement).ToArray();
            double[] times = new double[sizes.Length];

            string selectedAlgorithm = AlgorithmComboBox.SelectedItem.ToString();

            if (selectedAlgorithm == "Быстрая сортировка")
            {
                for (int i = 0; i < sizes.Length; i++)
                {
                    int size = sizes[i];
                    double totalTime = 0;


                    for (int run = 0; run < runs; run++)
                    {
                        int[] array = VectorGenerator.GenerateRandomVector(size);
                        QuickSort sorter = new QuickSort();
                        Stopwatch stopwatch = new Stopwatch();

                        // Замер времени сортировки
                        stopwatch.Start();
                        sorter.Sort(array);
                        stopwatch.Stop();

                        totalTime += stopwatch.Elapsed.TotalMilliseconds;
                    }

                    times[i] = totalTime / runs; // Среднее время
                }
            }
            else if (selectedAlgorithm == "Сортировка пузырьком")
            {
                for (int i = 0; i < sizes.Length; i++)
                {
                    int size = sizes[i];
                    double totalTime = 0;

                    for (int run = 0; run < runs; run++)
                    {
                        int[] array = VectorGenerator.GenerateRandomVector(size);
                        BubbleSort sorter = new BubbleSort();
                        Stopwatch stopwatch = new Stopwatch();

                        // Замер времени сортировки
                        stopwatch.Start();
                        sorter.Sort(array);
                        stopwatch.Stop();

                        totalTime += stopwatch.Elapsed.TotalMilliseconds;
                    }

                    times[i] = totalTime / runs; // Среднее время
                }
            }
            else if (selectedAlgorithm == "Сортировка обменом")
            {
                for (int i = 0; i < sizes.Length; i++)
                {
                    int size = sizes[i];
                    double totalTime = 0;

                    for (int run = 0; run < runs; run++)
                    {
                        int[] array = VectorGenerator.GenerateRandomVector(size);
                        ExchangeSort sorter = new ExchangeSort();
                        Stopwatch stopwatch = new Stopwatch();

                        // Замер времени сортировки
                        stopwatch.Start();
                        sorter.Sort(array);
                        stopwatch.Stop();

                        totalTime += stopwatch.Elapsed.TotalMilliseconds;
                    }

                    times[i] = totalTime / runs; // Среднее время
                }
            }
            else if (selectedAlgorithm == "Блинная сортировка")
            {
                for (int i = 0; i < sizes.Length; i++)
                {
                    int size = sizes[i];
                    double totalTime = 0;

                    for (int run = 0; run < runs; run++)
                    {
                        int[] array = VectorGenerator.GenerateRandomVector(size); 
                        PancakeSort sorter = new PancakeSort();
                        Stopwatch stopwatch = new Stopwatch();

                        // Замер времени сортировки
                        stopwatch.Start();
                        sorter.Sort(array); 
                        stopwatch.Stop();

                        totalTime += stopwatch.Elapsed.TotalMilliseconds;
                    }

                    times[i] = totalTime / runs; // Среднее время
                }
            }
            else if (selectedAlgorithm == "Гибридная сортировка")
            {
                for (int i = 0; i < sizes.Length; i++)
                {
                    int size = sizes[i];
                    double totalTime = 0;

                    for (int run = 0; run < runs; run++)
                    {
                        int[] array = VectorGenerator.GenerateRandomVector(size);
                        Timsort sorter = new Timsort();
                        Stopwatch stopwatch = new Stopwatch();

                        // Замер времени сортировки
                        stopwatch.Start();
                        sorter.Sort(array);
                        stopwatch.Stop();


                        totalTime += stopwatch.Elapsed.TotalMilliseconds;
                    }

                    times[i] = totalTime / runs; // Среднее время
                }
            }
            else if (selectedAlgorithm == "Постоянная функция")
            {
                ConstantFunction function = new ConstantFunction();
                for (int i = 0; i < sizes.Length; i++)
                {
                    int[] vector = VectorGenerator.GenerateRandomVector(sizes[i]);
                    double totalTime = 0;

                    for (int run = 0; run < runs; run++)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        function.Calculate(vector);
                        stopwatch.Stop();

                        totalTime += stopwatch.Elapsed.TotalMilliseconds;
                    }

                    times[i] = totalTime / runs;
                }
            }
            else if (selectedAlgorithm == "Умножение элементов")
            {
                MultiplicationOfElements multiplication = new MultiplicationOfElements();
                for (int i = 0; i < sizes.Length; i++)
                {
                    int[] vector = VectorGenerator.GenerateRandomVector(sizes[i]);
                    double totalTime = 0;

                    for (int run = 0; run < runs; run++)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        multiplication.Calculate(vector);
                        stopwatch.Stop();

                        totalTime += stopwatch.Elapsed.TotalMilliseconds;
                    }

                    times[i] = totalTime / runs;
                }
            }
            else if (selectedAlgorithm == "Сумма элементов")
            {
                SumOfElements sum = new SumOfElements();
                for (int i = 0; i < sizes.Length; i++)
                {
                    int[] vector = VectorGenerator.GenerateRandomVector(sizes[i]);
                    double totalTime = 0;

                    for (int run = 0; run < runs; run++)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        sum.Calculate(vector);
                        stopwatch.Stop();

                        totalTime += stopwatch.Elapsed.TotalMilliseconds;
                    }

                    times[i] = totalTime / runs;
                }
            }
            else if (selectedAlgorithm == "Прямое вычисление")
            {
                NaivePolynomialEvaluation naivePolynomial = new NaivePolynomialEvaluation();
                for (int i = 0; i < sizes.Length; i++)
                {
                    int[] coefficients = VectorGenerator.GenerateRandomVector(sizes[i]);
                    double totalTime = 0;

                    for (int run = 0; run < runs; run++)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        naivePolynomial.Calculate(coefficients, 1.0);
                        stopwatch.Stop();

                        totalTime += stopwatch.Elapsed.TotalMilliseconds;
                    }

                    times[i] = totalTime / runs;
                }
            }
            else if (selectedAlgorithm == "Схема Горнера")
            {
                HornerMethod hornerMethod = new HornerMethod();
                for (int i = 0; i < sizes.Length; i++)
                {
                    int[] coefficients = VectorGenerator.GenerateRandomVector(sizes[i]);
                    double totalTime = 0;


                    for (int run = 0; run < runs; run++)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        hornerMethod.Calculate(coefficients, 1.0);
                        stopwatch.Stop();

                        totalTime += stopwatch.Elapsed.TotalMilliseconds;
                    }

                    times[i] = totalTime / runs;
                }
            }
            else
            {
                MessageBox.Show("Данный алгоритм еще не реализован.");
                return;
            }

            // Строим график
            PlotGraph(sizes, times);
        }

        private void PlotGraph(int[] sizes, double[] times)
        {
            LineSeries lineSeries = new LineSeries
            {
                Values = new ChartValues<double>(times),
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 5
            };

            // Очищаем предыдущие данные графика
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

            // Настраиваем оси
            MyChart.AxisX.Clear();
            MyChart.AxisX.Add(new Axis
            {
                Title = "Размер массива",
                Labels = sizes.Select(x => x.ToString()).ToArray()
            });

            MyChart.AxisY.Clear();
            MyChart.AxisY.Add(new Axis
            {
                Title = "Время (мс)"
            });
        }
    }
}
