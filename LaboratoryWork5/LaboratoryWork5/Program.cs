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
            var resultLagr = GetResultLagr();
            var resultNuthon = GetResultNuthon();
            for (int i = 0; i < resultLagr.Length; i++)
                Console.WriteLine(resultLagr[i]);
            for (int i = 0; i < resultNuthon.Length; i++)
                Console.WriteLine("r" + i + " = " + (resultLagr[i] - resultNuthon[i]));
        }

        private static double[] GetResultNuthon()
        {
            var nuthonArray = new double[preassignedPoints[0].Length][];
            for (int i = 0; i < nuthonArray.Length; i++)
            {
                nuthonArray[i] = new double[nuthonArray.Length + 1];
                nuthonArray[i][0] = preassignedPoints[0][i];
                nuthonArray[i][1] = preassignedPoints[1][i];
            }

            for (int i = 2; i < nuthonArray[0].Length; i++)
            {
                for (int j = 0; j <= nuthonArray.Length - i; j++)
                {
                    nuthonArray[j][i] = (nuthonArray[j][i - 1] - nuthonArray[j + 1][i - 1]) /
                                        (nuthonArray[j][0] - nuthonArray[j + i - 1][0]);
                }
            }

            var results = new double[preassignedPoints[0].Length - 1][];
            for (int i = 0; i < results.Length; i++)
            {
                var member = new double[results.Length + 1];
                member[member.Length - 1] = 1;
                for (int j = 0; j <= i; j++)
                    member = AddMember(member, preassignedPoints[0][j]);
                for (int j = 0; j < member.Length; j++)
                    member[j] *= nuthonArray[0][i + 2];
                results[i] = member;
            }

            var resultNuthon = new double[results.Length + 1];
            resultNuthon[resultNuthon.Length - 1] = nuthonArray[0][1];
            for (int i = 0; i < resultNuthon.Length; i++)
            for (int j = 0; j < results.Length; j++)
                resultNuthon[i] += results[j][i];
            return resultNuthon;
        }

        private static double[] GetResultLagr()
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
                    foundation[i] = AddMember(foundation[i], preassignedPoints[0][j]);
                }

                for (int j = 0; j < foundation[i].Length; j++)
                    foundation[i][j] /= divider;
            }
            var resultLang = new double[preassignedPoints[0].Length]; 
            for (int i = 0; i < foundation[0].Length; i++)
            {
                var result = 0.0;
                for (int j = 0; j < foundation.Length; j++)
                    result += preassignedPoints[1][j] * foundation[j][i];
                resultLang[i] = result;
            }

            return resultLang;
        }

        private static double[] AddMember(double[] member, double coef)
        {
            var temp = member.ToArray();
            for (int k = 0; k < member.Length; k++)
                member[member.Length - 1 - k] *= -coef;
            for (int k = 0; k < member.Length - 1; k++)
                member[member.Length - 2 - k] += temp[member.Length - 1 - k];
            return member;
        }
    }
}