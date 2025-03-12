namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("коорды пушки (x, y)");
            int x0 = Convert.ToInt32(Console.ReadLine());
            int y0 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Начальная скорость v0");
            int v0 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("угол а (градусы)");
            double a = (Convert.ToInt32(Console.ReadLine()) * Math.PI) / 180;

            double vx0 = v0 * Math.Cos(a);
            double vy0 = v0 * Math.Sin(a);

            const double g = 9.81;

            double x = x0, y = y0;
            double t = 0;
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($" x | y | t");
            double t_total = Math.Round(2 * vy0 / g, 1);
            while (t <= t_total)
            {
                x = x0 + vx0 * t;
                y = y0 + vy0 * t - ((g * Math.Pow(t, 2)) / 2);

                
                Console.WriteLine($" {Math.Round(x, 2)} | {Math.Round(y, 2)} | {t} ");
                t = Math.Round(t + 0.1, 1);
            }

            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Общее время полета: {t_total} секунд");
        }
    }
}
