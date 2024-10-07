using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.Logic.Matrix
{
    public interface IMatrixGenerator
    {
        static abstract int[,] GenerateRandomMatrix(int n, int minvalue, int maxvalue);
    }
}
