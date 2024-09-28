using System;
using MyVectorLibrary.Sorters;

namespace MyLibrary.Logic.Algorithms
{
    public static class SorterFactory
    {
        public static ISorter GetSorter(SorterType sorterType)
        {
            return sorterType switch
            {
                SorterType.BubbleSort => new BubbleSort(),
                SorterType.QuickSort => new QuickSort(),
                SorterType.TimSort => new Timsort(),
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