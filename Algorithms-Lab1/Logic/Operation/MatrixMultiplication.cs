using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Operation
{
    public class MatrixMultiplication
    {
        public int[,] Multiply(int[,] matrixA, int[,] matrixB) 
        { 
            if (matrixA == null || matrixB == null) 
                throw new ArgumentNullException("Матрицы не могут быть null."); 

            int sizeA = matrixA.GetLength(0); 
            int sizeB = matrixB.GetLength(0); 

            if (sizeA != sizeB || sizeA  != matrixA.GetLength(1) || sizeB != matrixB.GetLength(1)) 
                throw new ArgumentException("Обе матрицы должны быть квадратными и одинакового размера.");
            
            int[,] resultMatrix = new int[sizeA, sizeA];

            for (int i = 0; i < sizeA; i++) 
            { 
                for (int j = 0; j < sizeA; j++) 
                { 
                    for (int k = 0; k < sizeA; k++) 
                    { 
                        resultMatrix[i, j] += matrixA[i, k] * matrixB[k, j]; 
                    } 
                } 
            } 
            
            return resultMatrix; 
        }
    }
}
