using System;
using System.Data;
using System.Linq;

namespace LaboratoryWork6
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var datas = new double[2][];
            datas[0] = new[] {1.0, 1.2, 1.4, 1.6, 1.8, 2.0};
            datas[1] = new[] {0.9, 2.0, 3.0, 3.8, 5.1, 5.8};
            ShowRoots(datas);
        }

        private static void ShowRoots(double[][] datas)
        {
            var matrix = GetMatrix(datas);
            var rootsC = new double[] {0}.Concat(GetRootsC(matrix)).ToArray();
            var rootsB = new double[rootsC.Length];
            for (int i = 0; i < rootsB.Length - 1; i++)
                rootsB[i] = (datas[1][i + 1] - datas[1][i]) / GetH(datas, i + 1) -
                            GetH(datas, i + 1) * (rootsC[i + 1] + 2 * rootsC[i]) / 3;
            rootsB[rootsB.Length - 1] =
                (datas[1][rootsB.Length - 1] - datas[1][rootsB.Length - 2]) / GetH(datas, rootsB.Length - 1) -
                GetH(datas, rootsB.Length - 1) * 2 * rootsC[rootsB.Length - 1] / 3;
            var rootsD = new double[rootsB.Length];
            for (int i = 0; i < rootsD.Length - 1; i++)
                rootsD[i] = (rootsC[i + 1] - rootsC[i]) / (3 * GetH(datas, i + 1));
            rootsD[rootsD.Length - 1] = -rootsC[rootsD.Length - 1] / (3 * GetH(datas, rootsD.Length - 1));
            var rootsA = datas[1].ToArray();
            for (int i = 0; i < rootsB.Length; i++)
                Console.WriteLine(
                    $"y{i + 1} = {rootsA[i]} + {rootsB[i]}(x - {datas[0][i]}) + {rootsC[i]}(x - {datas[0][i]})^2 + {rootsD[i]}(x - {datas[0][i]})^3");
        }

        private static double[][] GetMatrix(double[][] datas)
        {
            var matrix = new double[datas[0].Length - 2][];
            for (int i = 0; i < matrix.Length; i++)
            {
                var hi = GetH(datas, i + 2);
                var previousHi = GetH(datas, i + 1);
                var result = new double[matrix.Length + 1];
                if(i > 0)
                    result[i - 1] = previousHi;
                result[i] = 2 * (previousHi + hi);
                if(i < result.Length - 2)
                    result[i + 1] = hi;
                result[result.Length - 1] = 3 * ((datas[1][i + 2] - datas[1][i + 1]) / hi -
                                                 (datas[1][i + 1] - datas[1][i] / previousHi));
                matrix[i] = result;
            }

            return matrix;
        }
        
        
        private static double GetH(double[][] datas, int index) => datas[0][index] - datas[0][index - 1];
        
        private static double[][] GetCoeffs(double[][] matrix)
        {
            var result = new double[matrix.Length][];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new double[result.Length];
                result[i][result.Length - 1] = matrix[i].Last();
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

        private static double[] GetRootsC(double[][] matrix)
        {
            var coeffs = GetCoeffs(matrix);
            var runThroughCoeffs = GetRunThroughCoeffs(coeffs);
            var result = new double[runThroughCoeffs.Length];
            result[result.Length - 1] = runThroughCoeffs[runThroughCoeffs.Length - 1][1];
            for (int i = result.Length - 2; i >= 0; i--)
                result[i] = runThroughCoeffs[i][0] * result[i + 1] + runThroughCoeffs[i][1];

            return result;
        }
    }
}