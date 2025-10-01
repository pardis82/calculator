using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Program
    {
        
        static void Main(string[] args)
        {

            var (numbers, operators) = ReadFromConsole();

            if (numbers.Count == 0)
            {
                Console.WriteLine("No numbers entered.");
                return;
            }
            double result = EvaluateWithPrecedence(numbers, operators);

    Console.WriteLine($"Final Result: {result}");

        }


        static (List<double> numbers, List<string> operators) ReadFromConsole()
        {
            List<double> numbers = new List<double>();
            List<string> operators = new List<string>();
            while (true)
            {
                Console.Write($"Enter a number or type done when you're finished ");
                string inputnum = Console.ReadLine();
                if (inputnum.ToLower() == "done")
                {
                    break;
                }
                if (double.TryParse(inputnum, out double num))
                {
                    numbers.Add(num);
                    Console.Write("Enter your desired operation (+, -, *, /) or type done if you're finished: ");
                    string inputop = Console.ReadLine();
                    if (inputop.ToLower() == "done")
                    {
                        break;
                    }
                    else if (inputop == "+" || inputop == "*" || inputop == "/" || inputop == "-")
                    {
                        operators.Add(inputop);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid operator");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid input");
                }
            }

            return (numbers, operators);
        }

        static double Multiply(double a, double b) => a * b;
        static double Sum(double a, double b) => a + b;
        static double Subtract(double a, double b) => a - b;
        static double Divide(double a, double b)
        {
            if (b == 0) { Console.WriteLine("Error: Division by zero!"); return 0; }
            return a / b;
        }

        static double EvaluateWithPrecedence(List<double> numbers, List<string> operators)
        {
            List<double> tempNumbers = numbers.Select(n => (double)n).ToList();
            List<string> tempOperators = new List<string>(operators);

            for (int i = 0; i < tempOperators.Count; i++)
            {
                if (tempOperators[i] == "*" || tempOperators[i] == "/")
                {
                    double result = 0;
                    if (tempOperators[i] == "*")
                        result = Multiply(tempNumbers[i], tempNumbers[i + 1]);
                    else
                        result = Divide(tempNumbers[i], tempNumbers[i + 1]);

                    tempNumbers[i] = result;
                    tempNumbers.RemoveAt(i + 1);
                    tempOperators.RemoveAt(i);
                    i--;
                }
            }

            double finalResult = tempNumbers[0];
            for (int i = 0; i < tempOperators.Count; i++)
            {
                if (tempOperators[i] == "+")
                    finalResult = Sum(finalResult, tempNumbers[i + 1]);
                else
                    finalResult = Subtract(finalResult, tempNumbers[i + 1]);
            }

            return finalResult;
        }

    }
}
