using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Создаем график
            cartesianChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Пример данных",
                    Values = new ChartValues<double> { 3, 5, 7, 4, 6 }
                }
            };

            // Настройка осей
            cartesianChart.AxisX.Add(new Axis
            {
                Title = "X Axis",
                Labels = new[] { "A", "B", "C", "D", "E" }
            });

            cartesianChart.AxisY.Add(new Axis
            {
                Title = "Y Axis",
                LabelFormatter = value => value.ToString("N")
            });
            
            cartesianChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Серия 1",
                    Values = new ChartValues<double> { 3, 5, 7, 4, 6 }
                },
                new LineSeries
                {
                    Title = "Серия 2",
                    Values = new ChartValues<double> { 2, 6, 5, 7, 3 }
                }
            };
        }
    }
}