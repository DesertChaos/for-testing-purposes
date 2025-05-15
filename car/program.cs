using System;
using System.Collections.Generic;

class Program
{
    static string lastAction = "";
    static List<Avto> cars = new List<Avto>();

    static void Main()
    {
        for (int i = 0; i < 3; i++)
        {
            Avto newCar = new Avto();
            newCar.Info($"A{i + 1}23BC", 30 + i * 5, 7.5 + i);
            cars.Add(newCar);
        }

        Avto.SaveAllToFile(cars.ToArray(), "all_cars_data.txt");
        cars = new List<Avto>(Avto.LoadAllFromFile("all_cars_data.txt"));

        Console.WriteLine("Загруженные машины:");
        foreach (var car in cars)
        {
            car.Out();
            Console.WriteLine();
        }

        while (true)
        {
            Avto selectedCar = SelectCarMenu();
            if (selectedCar == null) break;

            CarControlMenu(selectedCar);
        }
    }

    static void ShowStatus(string currentCar = null)
    {
        Console.Clear();
        if (!string.IsNullOrEmpty(lastAction))
        {
            Console.WriteLine($"Последнее действие: {lastAction}");
            Console.WriteLine(new string('-', 30));
        }
        if (!string.IsNullOrEmpty(currentCar))
        {
            Console.WriteLine($"Текущая машина: {currentCar}\n");
        }
    }

    static Avto SelectCarMenu()
    {
        while (true)
        {
            ShowStatus();

            Console.WriteLine("Выберите машину для управления:");
            for (int i = 0; i < cars.Count; i++)
            {
                Console.WriteLine($"{i + 1} - Машина {cars[i].Nomer}");
            }
            Console.WriteLine("n - Создать новую машину");
            Console.WriteLine("0 - Выйти из программы");
            Console.Write("\nВаш выбор: ");

            string input = Console.ReadLine().ToLower();

            if (input == "0")
            {
                lastAction = "Выход из программы";
                return null;
            }
            else if (input == "n")
            {
                Avto newCar = CreateNewCar();
                cars.Add(newCar);
                lastAction = $"Создана новая машина: {newCar.Nomer}";
                continue;
            }
            else if (int.TryParse(input, out int choice) && choice > 0 && choice <= cars.Count)
            {
                Avto selectedCar = cars[choice - 1];
                lastAction = $"Выбрана машина: {selectedCar.Nomer}";
                return selectedCar;
            }

            lastAction = "Неверный выбор, попробуйте снова";
        }
    }

    static Avto CreateNewCar()
    {
        Console.Clear();
        Console.WriteLine("Создание новой машины:");

        Console.Write("Введите номер машины: ");
        string number = Console.ReadLine();

        Console.Write("Введите начальный пробег: ");
        int mileage = int.Parse(Console.ReadLine());

        Console.Write("Введите количество топлива: ");
        double fuel = double.Parse(Console.ReadLine());

        Avto newCar = new Avto();
        newCar.Info(number, mileage, fuel);

        Avto.SaveAllToFile(cars.ToArray(), "all_cars_data.txt");

        return newCar;
    }

    static void CarControlMenu(Avto selectedCar)
    {
        while (true)
        {
            ShowStatus(selectedCar.Nomer);

            Console.WriteLine("Меню управления:");
            Console.WriteLine("1 - Информация о машине");
            Console.WriteLine("2 - Двигаться");
            Console.WriteLine("3 - Заправиться");
            Console.WriteLine("4 - Въехать в аварию");
            Console.WriteLine("5 - Вернуться к выбору машины");
            Console.WriteLine("0 - Выйти из программы");
            Console.Write("\nЧто делаем: ");

            string input = Console.ReadLine();
            Console.Clear();

            switch (input)
            {
                case "1":
                    lastAction = "Просмотр информации о машине";
                    selectedCar.Out();
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Write("Введите расстояние для движения: ");
                    if (int.TryParse(Console.ReadLine(), out int distance))
                    {
                        selectedCar.Move(distance);
                        lastAction = $"Машина проехала {distance} км";
                    }
                    else
                    {
                        lastAction = "Ошибка: некорректное расстояние!";
                    }
                    break;
                case "3":
                    Console.Write("Введите количество топлива для заправки: ");
                    if (int.TryParse(Console.ReadLine(), out int fuel))
                    {
                        selectedCar.Zapravka(fuel);
                        lastAction = $"Заправлено {fuel} литров топлива";
                    }
                    else
                    {
                        lastAction = "Ошибка: некорректное количество топлива!";
                    }
                    break;
                case "4":
                    Avariya();
                    lastAction = "Произошла авария!";
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    break;
                case "5":
                    lastAction = "Возврат к выбору машины";
                    return;
                case "0":
                    lastAction = "Выход из программы";
                    Environment.Exit(0);
                    break;
                default:
                    lastAction = "Неизвестная команда";
                    break;
            }
        }
    }

    static void Avariya()
    {
        if (cars.Count < 2)
        {
            Console.WriteLine("Недостаточно машин для аварии");
            return;
        }

        Random rand = new Random();
        int index1 = rand.Next(cars.Count);
        int index2;
        do
        {
            index2 = rand.Next(cars.Count);
        } while (index2 == index1);

        Console.WriteLine($"\nАвария между машинами {cars[index1].Nomer} и {cars[index2].Nomer}!");
    }
}
