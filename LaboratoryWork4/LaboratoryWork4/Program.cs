using System;
using System.Linq;

namespace LaboratoryWork4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var matrix = new[]
            {
                new double[] {3, 1, 0, 0, 5},
                new double[] {1, 2, 1, 0, 6},
                new double[] {0, 3, 9, 6, 25},
                new double[] {0, 0, 2, 4, 5}
            };
            var roots = GetRoots(matrix);
            foreach (var root in roots)
                Console.WriteLine(root);
            Console.WriteLine();
            CheckResult(matrix, roots);
        }

        private static double[][] GetCoeffs(double[][] matrix)
        {
            var result = new double[matrix.Length][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new double[4];
                result[i][3] = matrix[i].Last();
                for (int j = 0; j < result[i].Length - 1; j++)
                    if(j + i - 1 >= 0 && j + i - 1 < result[i].Length)
                        result[i][j] = matrix[i][j + i - 1];
            }

            return result;
        }

        private static double[][] GetRunThroughCoeffs(double[][] coeffs)
        {
            var result = new double[coeffs.Length][];
            result[0] = new[] {-coeffs[0][2] / coeffs[0][1], coeffs[0][3] / coeffs[0][1]};
            for (int i = 1; i < result.Length; i++)
                result[i] = new[]
                {
                    -coeffs[i][2] / (coeffs[i][0] * result[i - 1][0] + coeffs[i][1]),
                    (coeffs[i][3] - coeffs[i][0] * result[i - 1][1]) / (coeffs[i][0] * result[i - 1][0] + coeffs[i][1])
                };
            return result;
        }

        private static double[] GetRoots(double[][] matrix)
        {
            var coeffs = GetCoeffs(matrix);
            var runThroughCoeffs = GetRunThroughCoeffs(coeffs);
            var result = new double[runThroughCoeffs.Length];
            result[result.Length - 1] = runThroughCoeffs[runThroughCoeffs.Length - 1][1];
            for (int i = result.Length - 2; i >= 0; i--)
                result[i] = runThroughCoeffs[i][0] * result[i + 1] + runThroughCoeffs[i][1];

            return result;
        }

        private static void CheckResult(double[][] matrix, double[] roots)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                var sum = 0.0;
                for (int j = 0; j < roots.Length; j++)
                    sum += matrix[i][j] * roots[j];
                Console.WriteLine("r" + (i + 1) + " = " + (matrix[i].Last() - sum));
            }
        }
    }
}