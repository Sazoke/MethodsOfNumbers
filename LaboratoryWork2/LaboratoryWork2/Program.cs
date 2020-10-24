using System;
using System.Linq;

namespace LaboratoryWork2
{
    internal class Program
    {
        private static double[] _parameters;
        private static double[] _parametersOfDerivative;
        private static double definition = 0.01;
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Введите параметры уравнения");
            _parameters = Console.ReadLine().Split(' ')
                .Select(n => double.Parse(n))
                .ToArray();
            _parametersOfDerivative = GetParametersOfDerivative(_parameters);
            Console.WriteLine("Введите левую границу");
            var left = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите правую границу");
            var right = double.Parse(Console.ReadLine());
            if(left >= right || GetResultOfFunction(left) * GetResultOfFunction(right) >= 0)
                throw new Exception("Неверный отрезок");
            Console.WriteLine(GetResultOfMethodDivisionSegments(0, 1));
            
            var firstValue = 0.0;
            var parametersOfDoubleDerivative = GetParametersOfDerivative(_parametersOfDerivative);
            if (GetResultOfFunction(left) *
                GetResultOfFunction(left, parametersOfDoubleDerivative) > 0)
                firstValue = left;
            else if (GetResultOfFunction(right) *
                GetResultOfFunction(right, parametersOfDoubleDerivative) > 0)
                firstValue = right;
            else
                throw new Exception("Неверный отрезок");
            Console.WriteLine(GetResultOfMethodNuton(firstValue));
        }

        private static double[] GetParametersOfDerivative(double[] parameters)
        {
            var result = new double[parameters.Length - 1];
            for (int i = 0; i < result.Length; i++)
                result[i] = parameters[i] * (parameters.Length - i - 1);
            return result;
        }

        private static double GetResultOfMethodDivisionSegments(double left, double right)
        {
            var middle = 0.0;
            while (right - left > definition)
            {
                middle = (right + left) / 2;
                var leftValue = GetResultOfFunction(left);
                var rightValue = GetResultOfFunction(right);
                var middleValue = GetResultOfFunction(middle);
                if (leftValue * middleValue < 0)
                    right = middle;
                else if (rightValue * middleValue < 0)
                    left = middle;
                else
                    return middle;
            }

            return middle;
        }

        private static double GetResultOfMethodNuton(double firstValue)
        {
            var secondValue = GetNextValue(firstValue);
            while (Math.Abs(secondValue - firstValue) > definition)
            {
                firstValue = secondValue;
                secondValue = GetNextValue(secondValue);
            }

            return secondValue;
        }

        private static double GetResultOfFunction(double value, double[] parameters = null)
        {
            if (parameters == null)
                parameters = _parameters;
            var result = 0.0;
            for (int i = 0; i < parameters.Length; i++)
                result += parameters[i] * Math.Pow(value, parameters.Length - i - 1);
            return result;
        }

        private static double GetNextValue(double value) =>
            value - GetResultOfFunction(value) / GetResultOfFunction(value, _parametersOfDerivative);
    }
}