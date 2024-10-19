namespace Algorithms_Lab1.Logic.Algorithms
{
    public static class SorterFactory
    {
        public static ISorter GetSorter(SorterType sorterType)
        {
            return sorterType switch
            {
                SorterType.BubbleSort => new BubbleSort(),
                SorterType.QuickSort => new QuickSort(),
                SorterType.TimSort => new TimSort(),
                SorterType.ExchangeSort => new ExchangeSort(),
                _ => throw new ArgumentException("Неподдерживаемый тип сортировщика.", nameof(sorterType)),
            };
        }
    }

    public enum SorterType
    {
        BubbleSort,
        QuickSort,
        TimSort,
        ExchangeSort
    }
}

