using System;
using System.Linq;

namespace LaboratoryWork5
{
    internal class Program
    {
        private static double[][] preassignedPoints = new double[][]
        {
            new double[] {-2, 0, 1},
            new double[] {4, 1, 3}
        };
        public static void Main(string[] args)
        {
            var foundation = new double[preassignedPoints[0].Length][];
            for (int i = 0; i < foundation.Length; i++)
                foundation[i] = new double[foundation.Length];

            for (int i = 0; i < foundation.Length; i++)
            {
                var divider = 1.0;
                var count = 0;
                foundation[i][foundation[i].Length - 1] = 1;
                for (int j = 0; j < foundation.Length; j++, count++)
                {
                    if (i == j)
                    {
                        count--;
                        continue;
                    }
                    
                    divider *= preassignedPoints[0][i] - preassignedPoints[0][j];
                    var temp = foundation[i].ToArray();
                    for (int k = 0; k < count + 1; k++)
                        foundation[i][foundation[i].Length - 1 - k] *= -preassignedPoints[0][j];
                    for (int k = 0; k < count + 1; k++)
                        foundation[i][foundation[i].Length - 2 - k] += temp[foundation[i].Length - 1 - k];
                }

                for (int j = 0; j < foundation[i].Length; j++)
                    foundation[i][j] /= divider;
            }

            for (int i = 0; i < foundation.Length; i++)
            {
                var result = 0.0;
                for (int j = 0; j < foundation.Length; j++)
                    result += preassignedPoints[1][j] * foundation[j][i];
                Console.WriteLine(result);
            }
        }
    }
}