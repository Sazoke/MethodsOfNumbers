using System;
using System.Linq;

namespace LaboratoryWork7
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var datas = new double[][]
            {
                new double[] {1, 2, 3, 5},
                new double[] {4, 5, 6, 8}
            };
            ShowEquation(datas);
        }

        private static void ShowEquation(double[][] datas)
        {
            var matrix = new double[datas[0].Length][];
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new double[7];
                matrix[i][0] = datas[0][i];
                matrix[i][4] = datas[1][i];
                for (int j = 1; j < 4; j++)
                    matrix[i][j] = matrix[i][j - 1] * matrix[i][0];
                for (int j = 0; j < 2; j++)
                    matrix[i][5 + j] = matrix[i][4 + j] * matrix[i][0];
            }
            var result = new double[matrix[0].Length];
            for (int i = 0; i < matrix[0].Length; i++)
                for (int j = 0; j < matrix.Length; j++)
                    result[i] += matrix[j][i];
            var equation = new double[3][];
            for (int i = 0; i < equation.Length; i++)
            {
                equation[i] = new double[4];
                equation[i][equation[i].Length - 1] = result[result.Length - 1 - i];
                for (int j = 0; j < 3; j++)
                    if (3 - j - i >= 0)
                        equation[i][j] = result[3 - j - i];
            }
            equation[equation.Length - 1][equation[equation.Length - 1].Length - 2] = datas[0].Length;
            var resultOfSystemOfEquations = GetResultOfSystemOfEquations(equation);
            Console.WriteLine($"y(x) = {resultOfSystemOfEquations[0]}x^2 + {resultOfSystemOfEquations[1]}x + {resultOfSystemOfEquations[2]}");
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