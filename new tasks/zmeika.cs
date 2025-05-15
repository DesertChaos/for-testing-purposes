using System;
using System.IO;

class Program
{
    static void Main()
    {
        int N = int.Parse(File.ReadAllText("C:\\Users\\gera2\\source\\repos\\task_15\\task_15\\INPUT.txt").Trim());
        Console.WriteLine($"N = {N}");

        int[,] matrix = new int[N, N];
        int num = 1;

        for (int diag = 0; diag < 2 * N - 1; diag++)
        {
            if (diag % 2 == 0)
            {
                int row = Math.Min(diag, N - 1);
                int col = diag - row;
                while (row >= 0 && col < N)
                {
                    matrix[col, row] = num++;
                    row--;
                    col++;
                }
            }
            else
            {
                int col = Math.Min(diag, N - 1);
                int row = diag - col;
                while (col >= 0 && row < N)
                {
                    matrix[col, row] = num++;
                    row++;
                    col--;
                }
            }
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
                Console.Write($"{matrix[i, j]} ");
            Console.WriteLine();
        }

        using (StreamWriter writer = new StreamWriter("C:\\Users\\gera2\\source\\repos\\task_15\\task_15\\OUTPUT.txt"))
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    writer.Write($"{matrix[i, j]} ");
                if (i < N - 1) writer.WriteLine();
            }
        }
    }
}
