using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Write("Введите разрядность магического квадрата: ");
        int N = int.Parse(Console.ReadLine());

        if (N == 1)
        {
            Console.WriteLine("1");
   
        }

        if (N % 2 == 0)
        {
            Console.WriteLine("Impossible");
            return;
        }

        int[,] magicSquare = new int[N, N];
        int num = 1, row = 0, col = N / 2;

        while (num <= N * N)
        {
            magicSquare[row, col] = num++;
            int newRow = (row - 1 + N) % N;
            int newCol = (col + 1) % N;
            if (magicSquare[newRow, newCol] != 0) row = (row + 1) % N;
            else { row = newRow; col = newCol; }
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
                Console.Write($"{magicSquare[i, j]} ");
            Console.WriteLine();
        }
    }
}.
