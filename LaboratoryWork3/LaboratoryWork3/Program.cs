using System;
using System.Linq;

namespace LaboratoryWork3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var matrix = new[]
            {
                new double [] {1, 2, -1, -1, 0},
                new double [] {2, 3, -1, 1, 3},
                new double [] {2, 5, 2, 1, 3},
                new double [] {3, 5, 1, 2, 5}
            };

            foreach (var value in GetResultOfSystemOfEquations(matrix))
                Console.WriteLine(value);
        }

        private static double[] GetResultOfSystemOfEquations(double[][] matrix)
        {
            var result = new double[matrix.Length];
            for (var i = 1; i < matrix.Length; i++)
            {
                var previous = matrix[i - 1];
                for (int j = i; j < matrix.Length; j++)
                {
                    var coefPrevious = previous[i - 1];
                    var coefNow = matrix[j][i - 1];
                    for (int k = i - 1; k < matrix[i].Length; k++)
                        matrix[j][k] = matrix[j][k] * coefPrevious - previous[k] * coefNow;
                }
            }

            for (int i = matrix.Length - 1; i >= 1; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    var coefPrevious = matrix[i][i];
                    var coefNow = matrix[j][i];
                    for (int k = 0; k <= i; k++)
                        matrix[j][k] = matrix[j][k] * coefPrevious - matrix[i][k] * coefNow;
                    matrix[j][matrix.Length] = matrix[j].Last() * coefPrevious - matrix[i].Last() * coefNow;
                }
            }

            for (int i = 0; i < matrix.Length; i++)
                result[i] = matrix[i].Last() / matrix[i][i];
            return result;
        }
    }
}