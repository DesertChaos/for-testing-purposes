using System;
using System.IO;
using System.Collections.Generic;

public class Avto
{
    private string nomer;
    private int benzina;
    private double rashod;
    private int probeg;
    private double x, y;

    public string Nomer => nomer;

    public void Info(string nom, int bak, double ras)
    {
        nomer = nom;
        benzina = bak;
        rashod = ras;
        probeg = 0;
        x = 0;
        y = 0;
    }

    public void Out()
    {
        Console.WriteLine($"Номер авто: {nomer}");
        Console.WriteLine($"Бензин в баке: {benzina} л");
        Console.WriteLine($"Расход топлива: {rashod} л/100 км");
        Console.WriteLine($"Пробег: {probeg} км");
    }

    public void Zapravka(float top)
    {
        benzina += (int)top;
        Console.WriteLine($"Заправлено {top} л. Текущий уровень: {benzina} л");
    }

    public void Move(int km)
    {
        double required = (rashod * km) / 100;
        if (required <= benzina)
        {
            benzina -= (int)required;
            probeg += km;
            CalculateDistance(km);
            Razgon();
            Console.WriteLine($"Проехали {km} км. Осталось топлива: {benzina} л");
            Tormozhenie();
        }
        else
        {
            Console.WriteLine("Недостаточно топлива для поездки");
        }
    }

    private int Ostatok()
    {
        return benzina;
    }

    public void Tormozhenie()
    {
        Console.WriteLine("Торможение...");
    }

    public void Razgon()
    {
        Console.WriteLine("Разгон...");
    }

    private void CalculateDistance(int km)
    {
        Random rand = new Random();
        x += rand.NextDouble() * km;
        y += rand.NextDouble() * km;
    }

    public void UpdateProbeg(int km)
    {
        probeg += km;
    }

    public static void SaveAllToFile(Avto[] cars, string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var car in cars)
            {
                writer.WriteLine(car.nomer);
                writer.WriteLine(car.benzina);
                writer.WriteLine(car.rashod);
                writer.WriteLine(car.probeg);
                writer.WriteLine(car.x);
                writer.WriteLine(car.y);
                writer.WriteLine("---");
            }
        }
        Console.WriteLine($"Данные всех машин сохранены в файл: {filename}");
    }

    public static Avto[] LoadAllFromFile(string filename)
    {
        List<Avto> loadedCars = new List<Avto>();

        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            Avto currentCar = null;
            int propertyCounter = 0;

            foreach (string line in lines)
            {
                if (line == "---")
                {
                    if (currentCar != null)
                    {
                        loadedCars.Add(currentCar);
                        currentCar = null;
                        propertyCounter = 0;
                    }
                    continue;
                }

                if (currentCar == null)
                {
                    currentCar = new Avto();
                }

                switch (propertyCounter)
                {
                    case 0: currentCar.nomer = line; break;
                    case 1: currentCar.benzina = int.Parse(line); break;
                    case 2: currentCar.rashod = double.Parse(line); break;
                    case 3: currentCar.probeg = int.Parse(line); break;
                    case 4: currentCar.x = double.Parse(line); break;
                    case 5: currentCar.y = double.Parse(line); break;
                }
                propertyCounter++;
            }

            Console.WriteLine($"Данные восстановлены из файла: {filename}");
            return loadedCars.ToArray();
        }
        else
        {
            Console.WriteLine($"Файл не найден: {filename}");
            return new Avto[0];
        }
    }
}
