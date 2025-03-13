using System;
using System.IO;

public class Program
{
    public static double F(double x)
    {
        return 2 * x * x + 3 * x;
    }

    public static double Integrate(double a, double b, int n)
    {
        double h = (b - a) / n;
        double sum = 0;
        for (int i = 1; i < n; i++)
        {
            double x = a + i * h;
            sum += F(x);
        }
        double result = h * sum;
        return result;
    }

    public static void Main(string[] args)
    {
        double a, b;
        int n;

        Console.Write("Вводите х: ");
        while (!double.TryParse(Console.ReadLine(), out a)) 
        {
            Console.WriteLine("Ввод неверен");
            Console.Write("Вводите х: ");
        }

        Console.Write("Вводите y: ");
        while (!double.TryParse(Console.ReadLine(), out b))
        {
            Console.WriteLine("Ввод неверен");
            Console.Write("Вводите y: ");
        }

        Console.Write("Вводите n: ");
        while (!int.TryParse(Console.ReadLine(), out n))
        {
            Console.WriteLine("Ввод неверен");
            Console.Write("Вводите n: ");
        }

        double result = Integrate(a, b, n);
        Console.WriteLine($"Определенный интеграл от {a} до {b} приверно равен: {Math.Round(result, 2)}");
    }
}
