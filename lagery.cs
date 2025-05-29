using System;
using System.Collections.Generic;
using System.Linq;

namespace CampBattleGame
{
    public class Character
    {
        public string Name { get; set; }
        public char Camp { get; set; } // 'A' или 'B'
        public int X { get; set; }
        public int Y { get; set; }
        public int Damage { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public void FillInfo()
        {
            Console.Write("Введите имя персонажа: ");
            Name = Console.ReadLine();

            while (true)
            {
                Console.Write("Выберите лагерь (A/B): ");
                string input = Console.ReadLine().ToUpper();
                if (input == "A" || input == "B")
                {
                    Camp = input[0];
                    break;
                }
                Console.WriteLine("Некорректный ввод. Используйте только A или B.");
            }

            while (true)
            {
                Console.Write("Введите начальную координату X (0-10): ");
                X = int.Parse(Console.ReadLine());
                if (X >= 0 && X <= 10) break;
                Console.WriteLine("Координата X должна быть от 0 до 10!");
            }

            while (true)
            {
                Console.Write("Введите начальную координату Y (0-10): ");
                Y = int.Parse(Console.ReadLine());
                if (Y >= 0 && Y <= 10) break;
                Console.WriteLine("Координата Y должна быть от 0 до 10!");
            }

            Console.Write("Введите урон: ");
            Damage = int.Parse(Console.ReadLine());

            Console.Write("Введите здоровье: ");
            Health = int.Parse(Console.ReadLine());
            MaxHealth = Health;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Имя: {Name}, Лагерь: {Camp}");
            Console.WriteLine($"Координаты: ({X}, {Y})");
            Console.WriteLine($"Урон: {Damage}, Здоровье: {Health}/{MaxHealth}");
        }

        public void MoveHorizontal(int distance)
        {
            int newX = X + distance;
            if (newX < 0) newX = 0;
            if (newX > 10) newX = 10;

            Console.WriteLine($"{Name} переместился на {distance} по X. Новые координаты: ({newX}, {Y})");
            X = newX;
        }

        public void MoveVertical(int distance)
        {
            int newY = Y + distance;
            if (newY < 0) newY = 0;
            if (newY > 10) newY = 10;

            Console.WriteLine($"{Name} переместился на {distance} по Y. Новые координаты: ({X}, {newY})");
            Y = newY;
        }

        public void DealDamage(Character target, int damage)
        {
            Console.WriteLine($"{Name} наносит {damage} урона {target.Name}!");
            target.TakeDamage(damage);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
            Console.WriteLine($"{Name} получает {damage} урона. Осталось здоровья: {Health}");
        }

        public void FullHeal()
        {
            Health = MaxHealth;
            Console.WriteLine($"{Name} полностью восстановил здоровье: {Health}/{MaxHealth}");
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public bool IsAllyNearby(List<Character> characters)
        {
            return characters.Any(c =>
                c != this &&
                c.Camp == Camp &&
                c.IsAlive() &&
                Math.Abs(c.X - X) <= 1 &&
                Math.Abs(c.Y - Y) <= 1);
        }

        public List<Character> GetNearbyAllies(List<Character> characters)
        {
            return characters.Where(c =>
                c != this &&
                c.Camp == Camp &&
                c.IsAlive() &&
                Math.Abs(c.X - X) <= 1 &&
                Math.Abs(c.Y - Y) <= 1).ToList();
        }
    }

    class Program
    {
        static List<Character> characters = new List<Character>();
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в игру 'Битва лагерей'!");
            Console.WriteLine("Карта ограничена координатами от (0,0) до (10,10)");

            while (true)
            {
                PrintMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ClearAndAddCharacter();
                        break;
                    case "2":
                        ClearAndPrintCharactersInfo();
                        break;
                    case "3":
                        ClearAndMoveCharacter();
                        break;
                    case "4":
                        ClearAndDeleteCharacter();
                        break;
                    case "5":
                        ClearAndStartBattle();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ClearScreen()
        {
            Console.Clear();
            Console.WriteLine("=== Битва лагерей ===");
        }

        static void PrintMenu()
        {
            ClearScreen();
            Console.WriteLine("\nГлавное меню:");
            Console.WriteLine("1. Добавить персонажа");
            Console.WriteLine("2. Показать информацию о персонажах");
            Console.WriteLine("3. Переместить персонажа");
            Console.WriteLine("4. Удалить персонажа");
            Console.WriteLine("5. Начать битву");
            Console.WriteLine("6. Выход");
            Console.Write("Выберите действие: ");
        }

        static int CheckCampCount(char camp)
        {
            return characters.Count(c => c.Camp == camp);
        }

        static void ClearAndAddCharacter()
        {
            ClearScreen();
            Console.WriteLine("\nДобавление нового персонажа");

            var character = new Character();
            character.FillInfo();

            if ((character.Camp == 'A' && CheckCampCount('A') >= 4) ||
                (character.Camp == 'B' && CheckCampCount('B') >= 4))
            {
                Console.WriteLine($"Лагерь {character.Camp} уже содержит 4 персонажа! Персонаж не добавлен.");
                Console.ReadKey();
                return;
            }

            characters.Add(character);
            Console.WriteLine($"Персонаж {character.Name} добавлен в лагерь {character.Camp}!");
            Console.WriteLine($"Теперь в лагере A: {CheckCampCount('A')}/4, в лагере B: {CheckCampCount('B')}/4");
            Console.ReadKey();
        }

        static void ClearAndPrintCharactersInfo()
        {
            ClearScreen();
            if (characters.Count == 0)
            {
                Console.WriteLine("Сначала создайте персонажей!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nИнформация о персонажах:");
            Console.WriteLine("Лагерь A:");
            foreach (var character in characters.Where(c => c.Camp == 'A'))
            {
                character.PrintInfo();
                Console.WriteLine();
            }

            Console.WriteLine("Лагерь B:");
            foreach (var character in characters.Where(c => c.Camp == 'B'))
            {
                character.PrintInfo();
                Console.WriteLine();
            }

            DisplayMap();
            Console.ReadKey();
        }

        static void DisplayMap()
        {
            Console.WriteLine("\nКарта битвы (10x10):");
            Console.WriteLine("  0 1 2 3 4 5 6 7 8 9 10");

            for (int y = 0; y <= 10; y++)
            {
                Console.Write($"{y,2}");
                for (int x = 0; x <= 10; x++)
                {
                    var charsAtPos = characters.Where(c => c.X == x && c.Y == y).ToList();
                    if (charsAtPos.Count > 0)
                    {
                        Console.Write(charsAtPos[0].Camp);
                        if (charsAtPos.Count > 1) Console.Write('+');
                        else Console.Write(' ');
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
        }

        static void ClearAndMoveCharacter()
        {
            ClearScreen();
            if (characters.Count == 0)
            {
                Console.WriteLine("Сначала создайте персонажей!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nСписок персонажей:");
            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characters[i].Name} ({characters[i].Camp})");
            }

            Console.Write("Выберите персонажа для перемещения: ");
            if (!int.TryParse(Console.ReadLine(), out int charIndex) || charIndex < 1 || charIndex > characters.Count)
            {
                Console.WriteLine("Неверный выбор персонажа.");
                Console.ReadKey();
                return;
            }

            var character = characters[charIndex - 1];

            Console.WriteLine("Выберите направление:");
            Console.WriteLine("1. Вверх");
            Console.WriteLine("2. Вниз");
            Console.WriteLine("3. Влево");
            Console.WriteLine("4. Вправо");
            Console.Write("Ваш выбор: ");
            string direction = Console.ReadLine();

            Console.Write("Введите расстояние: ");
            if (!int.TryParse(Console.ReadLine(), out int distance))
            {
                Console.WriteLine("Некорректное расстояние.");
                Console.ReadKey();
                return;
            }

            switch (direction)
            {
                case "1":
                    character.MoveVertical(distance);
                    break;
                case "2":
                    character.MoveVertical(-distance);
                    break;
                case "3":
                    character.MoveHorizontal(-distance);
                    break;
                case "4":
                    character.MoveHorizontal(distance);
                    break;
                default:
                    Console.WriteLine("Неверное направление.");
                    break;
            }

            DisplayMap();
            Console.ReadKey();
        }

        static void ClearAndDeleteCharacter()
        {
            ClearScreen();
            if (characters.Count == 0)
            {
                Console.WriteLine("Нет персонажей для удаления!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nСписок персонажей:");
            for (int i = 0; i < characters.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {characters[i].Name} ({characters[i].Camp})");
            }

            Console.Write("Выберите персонажа для удаления (0 - отмена): ");
            if (!int.TryParse(Console.ReadLine(), out int charIndex) || charIndex < 0 || charIndex > characters.Count)
            {
                Console.WriteLine("Неверный выбор персонажа.");
                Console.ReadKey();
                return;
            }

            if (charIndex == 0)
            {
                Console.WriteLine("Удаление отменено.");
                Console.ReadKey();
                return;
            }

            var character = characters[charIndex - 1];
            characters.RemoveAt(charIndex - 1);
            Console.WriteLine($"Персонаж {character.Name} удален!");
            Console.ReadKey();
        }

        static void ClearAndStartBattle()
        {
            ClearScreen();
            if (characters.Count == 0)
            {
                Console.WriteLine("Сначала создайте персонажей!");
                Console.ReadKey();
                return;
            }
            if (CheckCampCount('A') < 1 || CheckCampCount('B') < 1)
            {
                Console.WriteLine("В каждом лагере должен быть хотя бы один персонаж!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nНачало битвы!");
            DisplayMap();
            Console.ReadKey();

            while (true)
            {
                ClearScreen();
                var campAAlive = characters.Count(c => c.Camp == 'A' && c.IsAlive());
                var campBAlive = characters.Count(c => c.Camp == 'B' && c.IsAlive());

                if (campAAlive == 0)
                {
                    Console.WriteLine("Лагерь B победил!");
                    RestoreHealthAfterBattle();
                    Console.ReadKey();
                    break;
                }

                if (campBAlive == 0)
                {
                    Console.WriteLine("Лагерь A победил!");
                    RestoreHealthAfterBattle();
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine("\n=== Ход лагеря A ===");
                ProcessCampTurn('A');
                DisplayMap();
                Console.ReadKey();

                campBAlive = characters.Count(c => c.Camp == 'B' && c.IsAlive());
                if (campBAlive == 0)
                {
                    ClearScreen();
                    Console.WriteLine("Лагерь A победил!");
                    RestoreHealthAfterBattle();
                    Console.ReadKey();
                    break;
                }

                ClearScreen();
                Console.WriteLine("\n=== Ход лагеря B ===");
                ProcessCampTurn('B');
                DisplayMap();
                Console.ReadKey();
            }
        }

        static void RestoreHealthAfterBattle()
        {
            Console.WriteLine("\nВосстановление здоровья всех персонажей:");
            foreach (var character in characters)
            {
                if (character.IsAlive())
                {
                    character.FullHeal();
                }
            }
        }

        static void ProcessCampTurn(char camp)
        {
            var campCharacters = characters.Where(c => c.Camp == camp && c.IsAlive()).ToList();
            var enemies = characters.Where(c => c.Camp != camp && c.IsAlive()).ToList();

            foreach (var attacker in campCharacters)
            {
                if (!attacker.IsAlive()) continue;

                var target = FindNearestEnemy(attacker, enemies);
                if (target == null)
                {
                    Console.WriteLine($"{attacker.Name} не нашел врагов для атаки.");
                    continue;
                }

                var allies = attacker.GetNearbyAllies(characters);
                bool hasAlliesNearby = allies.Any();

                if (hasAlliesNearby)
                {
                    int combinedDamage = attacker.Damage + allies.Sum(a => a.Damage);
                    Console.WriteLine($"{attacker.Name} с союзниками наносит {combinedDamage} урона {target.Name}!");
                    attacker.DealDamage(target, combinedDamage);

                    if (target.IsAlive())
                    {
                        int damagePerAlly = target.Damage / (1 + allies.Count);
                        Console.WriteLine($"{target.Name} контратакует с уроном {damagePerAlly} на каждого!");

                        attacker.TakeDamage(damagePerAlly);
                        foreach (var ally in allies)
                        {
                            ally.TakeDamage(damagePerAlly);
                        }
                    }
                }
                else
                {
                    attacker.DealDamage(target, attacker.Damage);

                    if (target.IsAlive())
                    {
                        target.DealDamage(attacker, target.Damage);
                    }
                }

                if (attacker.IsAlive() && target.IsAlive())
                {
                    Console.Write("\nПереместить персонажа? (Enter - нет, Y - да): ");
                    string moveChoice = Console.ReadLine().ToUpper();

                    if (moveChoice == "Y")
                    {
                        MoveCharacterAfterBattle(attacker);
                    }
                }
            }
        }

        static Character FindNearestEnemy(Character attacker, List<Character> enemies)
        {
            return enemies
                .Where(e => e.IsAlive())
                .OrderBy(e => Math.Abs(e.X - attacker.X) + Math.Abs(e.Y - attacker.Y))
                .FirstOrDefault();
        }

        static void MoveCharacterAfterBattle(Character character)
        {
            Console.WriteLine($"\nПеремещение персонажа {character.Name}:");
            Console.WriteLine("1. Вверх");
            Console.WriteLine("2. Вниз");
            Console.WriteLine("3. Влево");
            Console.WriteLine("4. Вправо");
            Console.Write("Выберите направление: ");
            string direction = Console.ReadLine();

            int distance = 1;

            switch (direction)
            {
                case "1":
                    character.MoveVertical(distance);
                    break;
                case "2":
                    character.MoveVertical(-distance);
                    break;
                case "3":
                    character.MoveHorizontal(-distance);
                    break;
                case "4":
                    character.MoveHorizontal(distance);
                    break;
                default:
                    Console.WriteLine("Персонаж остаётся на месте.");
                    break;
            }
        }
    }
}
