using System.Diagnostics;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Controls;
using MyLibrary.Logic.Vector;
using MyLibrary.Logic.Algorithms;
using MyVectorLibrary.Sorters;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            
        }

        private void ButtonСalculation_Click(object sender, RoutedEventArgs e)
        {
            int runs = 5; // Количество повторных запусков для усреднения времени
            int[] sizes = Enumerable.Range(1, 100).Select(x => x * 20).ToArray(); // Размеры массивов от 100 до 2000
            double[] times = new double[sizes.Length];

            // Запускаем замеры для QuickSort
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

            // Строим график
            PlotGraph(sizes, times);
            
            MessageBox.Show("Кнопка нажата!");
        }
        
        private void PlotGraph(int[] sizes, double[] times)
        {
            LineSeries lineSeries = new LineSeries
            {
                Title = "QuickSort",
                Values = new ChartValues<double>(times),
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 5
            };

            // Очищаем предыдущие данные графика
            MyChart.Series.Clear();
            MyChart.Series.Add(lineSeries);

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