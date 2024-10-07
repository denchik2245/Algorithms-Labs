using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Logic.Matrix
{
    public interface IMatrixGenerator
    {
        static abstract int[,] GenerateRandomSquareMatrix(int size, int minvalue, int maxvalue);
    }
}
