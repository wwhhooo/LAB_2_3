using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Matrix
{
    private int[,] data;
    private static Random rand = new Random();

    public Matrix(int rows, int cols)
    {
        data = new int[rows, cols];
    }

    public int GetValue(int row, int col)
    {
        return data[row, col];
    }

    public void SetValue(int row, int col, int value)
    {
        data[row, col] = value;
    }

    public int GetDimension(int dimension)
    {
        return data.GetLength(dimension);
    }

    // Конструктор для заполнения массива данными, вводимыми с клавиатуры
    public Matrix(int rows, int cols, double flag)
    {
        if (flag == 0)
        {
            data = new int[rows, cols];
            for (int j = 0; j < cols; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    Console.Write($"Введите элемент [{i}][{j}]: ");
                    data[i, j] = int.Parse(Console.ReadLine());
                }
            }
        }
        else
        {
            data = new int[rows, cols];
            for (int j = 0; j < cols; j++)
            {
                for (int i = 0; i < rows; i++)
                {
                    data[i, j] = rand.Next(10);
                }
            }
        }
    }

    // Конструктор для заполнения массива четырехзначными случайными числами, составленными из четных цифр
    public Matrix(int size, double flag)
    {
        data = new int[size, size];
        if (flag == 0)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    data[i, j] = GenerateRandomNumber();
                }
            }
        }
        else
        {
            for (int i = 0; i < size; i += 2)
            {
                for (int j = 0; j + i < size; j++)
                {
                    data[j, i + j] = j + 1;
                }
            }
            for (int i = 1; i < size; i += 2)
            {
                for (int j = 0; j + i < size; j++)
                {
                    data[j, i + j] = size - i - j;
                }
            }
        }
    }

    // Метод для генерации четырехзначного случайного числа, составленного из четных цифр
    private int GenerateRandomNumber()
    {
        int number = rand.Next(1, 5) * 2; // Генерирует случайное четное число от 2 до 8;

        for (int i = 0; i < 3; i++)
        {
            int digit = rand.Next(0, 5) * 2; // Генерирует случайное четное число от 0 до 8
            number = number * 10 + digit;
        }
        return number;
    }

    // метод для нахождения пути
    public int FindShortestPath()
    {
        int min = int.MaxValue, max = int.MinValue;
        int minRow = 0, minCol = 0, maxRow = 0, maxCol = 0;

        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                if (data[i, j] < min)
                {
                    min = data[i, j];
                    minRow = i;
                    minCol = j;
                }
                if (data[i, j] > max)
                {
                    max = data[i, j];
                    maxRow = i;
                    maxCol = j;
                }
            }
        }

        return Math.Max(Math.Abs(maxRow - minRow), Math.Abs(maxCol - minCol));
    }

    // метод для транспонирования матрицы
    private void Transpose()
    {
        int rows = data.GetLength(0);
        int cols = data.GetLength(1);

        int[,] transposedData = new int[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                transposedData[j, i] = data[i, j];
            }
        }

        data = transposedData;
    }

    // метод для подсчёта
    private void Compute(Matrix A, Matrix B, Matrix C)
    {
        int size = data.GetLength(0);
        int[,] result = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[i, j] = B.GetValue(i, j) + C.GetValue(i, j);
                result[i, j] = A.GetValue(i, j) - result[i, j];
            }
        }
        data = result;
    }

    // Метод для вывода массива на экран
    public void Display()
    {
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                Console.Write("{0,6}", data[i, j]);
            }
            Console.WriteLine();
        }
    }

    public static void Main(string[] args)
    {
        // 1 задание
        // Пример использования первого конструктора
        int rows, cols;
        Console.WriteLine("Введите количество строк (>0):");
        if (!int.TryParse(Console.ReadLine(), out rows))
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
        }
        if (rows > 0)
        {
            Console.WriteLine("Введите количество столбцов (>0):");
            if (!int.TryParse(Console.ReadLine(), out cols))
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
            }
            if (cols > 0)
            {
                Matrix matrix1 = new Matrix(rows, cols, 0.0);
                Console.WriteLine("1-ый Массив, заполненный с клавиатуры:");
                matrix1.Display();
            }
            else
            {
                Console.WriteLine("Количество столбцов < 0");
            }
        }
        else
        {
            Console.WriteLine("Количество строк < 0");
        }

        // Пример использования второго конструктора
        Console.WriteLine("Введите размер матрицы (>0):");
        if (!int.TryParse(Console.ReadLine(), out rows))
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
        }
        if (rows > 0)
        {
            Matrix matrix2 = new Matrix(rows, 0.0);
            Console.WriteLine("2-ой Массив, заполненный случайными четырехзначными числами из четных цифр:");
            matrix2.Display();
        }
        else
        {
            Console.WriteLine("Размер матрицы < 0");
        }

        // Пример использования третьего конструктора
        Console.WriteLine("Введите размер матрицы (>0):");
        if (!int.TryParse(Console.ReadLine(), out rows))
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
        }
        if (rows > 0)
        {
            Matrix matrix3 = new Matrix(rows, 0.1);
            Console.WriteLine("3-ий Массив, заполненный для произвольного размера по примеру");
            matrix3.Display();
        }
        else
        {
            Console.WriteLine("Размер матрицы < 0");
        }

        // 2 задание
        Console.WriteLine("Элементами массива:");
        Matrix matrix4 = new Matrix(rows, 0.0);
        matrix4.Display();
        Console.WriteLine("Самый короткий путь между максимальным и минимальным элементами массива:");
        Console.WriteLine(matrix4.FindShortestPath());
        Console.WriteLine();

        // 3 задание
        Console.WriteLine("Посчитаем Ат-(В+Ст)");
        Console.WriteLine("Введите размерность n x n");
        Console.WriteLine("Введите n(>0):");
        if (!int.TryParse(Console.ReadLine(), out rows))
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
        }
        if (rows > 0)
        {
            Matrix A = new Matrix(rows, rows, 0.1);
            Matrix B = new Matrix(rows, rows, 0.1);
            Matrix C = new Matrix(rows, rows, 0.1);
            Matrix result = new Matrix(rows, rows);
            Console.WriteLine("Матрица A");
            A.Display();
            Console.WriteLine("Матрица B");
            B.Display();
            Console.WriteLine("Матрица C");
            C.Display();
            // Вызовы методов транспонирования
            A.Transpose();
            Console.WriteLine("Матрица Aт");
            A.Display();
            C.Transpose();
            Console.WriteLine("Матрица Cт");
            C.Display();
            // Вызов метода подсчёта матриц
            Console.WriteLine(" Ат - (В + Ст) = ");
            result.Compute(A, B, C);
            result.Display();
        }
        else
        {
            Console.WriteLine("n < 0");
        }
        ToyProgram.ToyMain();
    }
}

public class Toy
{
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public int MinAge { get; set; }
    public int MaxAge { get; set; }
}

public class ToyProgram
{
    private const string FileName = "toys.json";
    private const string TextFileName = "numbers.txt";
    private static Random rand = new Random();

    public static void ToyMain()
    {
        // 4 задание
        Console.WriteLine("Введите значение k:");
        if (int.TryParse(Console.ReadLine(), out int k))
        {
            Console.WriteLine("Введите количество случайных чисел для заполнения исходного файла:");
            if (int.TryParse(Console.ReadLine(), out int count))
            {
                CreateSourceFile("1.txt", count);
                FilterAndWriteToTargetFile("1.txt", "2.txt", k);
                Console.WriteLine("Операция завершена успешно.");
            }
            else
            {
                Console.WriteLine("Некорректное значение количества чисел. Пожалуйста, введите целое число.");
            }
        }
        else
        {
            Console.WriteLine("Некорректное значение k. Пожалуйста, введите целое число.");
        }

        // 5 задание
        if (!File.Exists(FileName))
        {
            CreateFile();
        }
        Console.WriteLine("Список игрушек:");
        PrintToys();
        Console.WriteLine("Максимальная цена на игрушку: " + GetMostCost());

        // 6 задание
        // Заполняем файл случайными числами
        FileRandom("numbers(6).txt", 10);

        // Читаем содержимое файла и сохраняем его в список
        List<int> numbers = new List<int>();
        using (StreamReader reader = new StreamReader("numbers(6).txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (int.TryParse(line.Trim(), out int number))
                {
                    numbers.Add(number);
                }
            }
        }

        // Находим сумму элементов, которые равны своему индексу
        int sumOfElements = 0;
        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] == i)
            {
                sumOfElements += numbers[i];
            }
        }

        // Выводим полученную сумму
        Console.WriteLine("Сумма элементов, равных своему индексу: " + sumOfElements);

        // 7 задание
        // Заполнение текстового файла случайными данными
        FillTextRandomData();

        // Вычисление произведения элементов, которые кратны заданному числу k
        Console.Write("Введите число k: ");
        k = int.Parse(Console.ReadLine());
        Console.WriteLine("Произведение элементов, кратных " + k + ": " + CalculateMultiples(k));

        // 8 задание
        // Заполнение текстового файла случайными данными
        TextRandomData();
        Console.WriteLine("Создание рандомной строки в файле");
        // Переписывание строк без русских букв в другой файл
        FilterAndWriteToFile();
        Console.WriteLine("Переписывание его без русских букв.");
        Console.WriteLine("Успешно!");

        Console.ReadKey();
    }

    // 4 задание
    public static void CreateSourceFile(string fileName, int count)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
        {
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                int number = rand.Next(1, 100); // Генерация случайных чисел от 1 до 99
                writer.Write(number);
            }
        }
    }

    public static void FilterAndWriteToTargetFile(string sourceFileName, string targetFileName, int k)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(sourceFileName, FileMode.Open)))
        using (BinaryWriter writer = new BinaryWriter(File.Open(targetFileName, FileMode.Create)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int number = reader.ReadInt32();
                if (number % k == 0)
                {
                    writer.Write(number);
                }
            }
        }
    }

    private static void CreateFile()
    {
        int numberOfToys = rand.Next(5, 15); // Генерация случайного количества игрушек от 5 до 14
        Toy[] toys = new Toy[numberOfToys];

        for (int i = 0; i < numberOfToys; i++)
        {
            toys[i] = new Toy
            {
                Name = $"Constructor{i + 1}",
                Cost = rand.Next(50, 500), // Генерация случайной стоимости от 50 до 499
                MinAge = rand.Next(1, 5), // Генерация случайного минимального возраста от 1 до 4
                MaxAge = rand.Next(6, 10) // Генерация случайного максимального возраста от 6 до 9
            };
        }

        string jsonString = JsonSerializer.Serialize(toys);
        File.WriteAllText(FileName, jsonString);
    }

    // 5 задание
    private static decimal GetMostCost()
    {
        string jsonString = File.ReadAllText(FileName);
        Toy[] toys = JsonSerializer.Deserialize<Toy[]>(jsonString);

        decimal maxCost = 0;
        foreach (Toy toy in toys)
        {
            if (toy.Name.Contains("Constructor") && toy.Cost > maxCost)
            {
                maxCost = toy.Cost;
            }
        }

        return maxCost;
    }

    private static void PrintToys()
    {
        string jsonString = File.ReadAllText(FileName);
        Toy[] toys = JsonSerializer.Deserialize<Toy[]>(jsonString);

        foreach (Toy toy in toys)
        {
            Console.WriteLine($"Name: {toy.Name}, Cost: {toy.Cost}, MinAge: {toy.MinAge}, MaxAge: {toy.MaxAge}");
        }
    }

    // 6 задание
    // Метод для заполнения файла случайными числами
    public static void FileRandom(string filename, int count)
    {
        Random rand = new Random();
        using (StreamWriter writer = new StreamWriter(filename))
        {
            for (int i = 0; i < count; i++)
            {
                writer.Write(rand.Next(0, 11) + "");
            }
        }
    }

    // 7 задание
    private static void FillTextRandomData()
    {
        int numberOfLines = rand.Next(5, 15); // Генерация случайного количества строк от 5 до 14
        using (StreamWriter writer = new StreamWriter(TextFileName))
        {
            for (int i = 0; i < numberOfLines; i++)
            {
                int numberOfNumbers = rand.Next(3, 10); // Генерация случайного количества чисел в строке от 3 до 9
                int[] numbers = new int[numberOfNumbers];
                for (int j = 0; j < numberOfNumbers; j++)
                {
                    numbers[j] = rand.Next(1, 100); // Генерация случайного числа от 1 до 99
                }
                writer.WriteLine(string.Join(" ", numbers));
            }
        }
    }

    private static long CalculateMultiples(int k)
    {
        long product = 1;
        bool found = false;

        using (StreamReader reader = new StreamReader(TextFileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                int[] numbers = line.Split(' ').Select(int.Parse).ToArray();
                foreach (int number in numbers)
                {
                    if (number % k == 0)
                    {
                        product *= number;
                        found = true;
                    }
                }
            }
        }

        return found ? product : 0;
    }

    // 8 задание
    private static void TextRandomData()
    {
        int numberOfLines = rand.Next(5, 15); // Генерация случайного количества строк от 5 до 14
        using (StreamWriter writer = new StreamWriter("input.txt"))
        {
            for (int i = 0; i < numberOfLines; i++)
            {
                int length = rand.Next(5, 20); // Генерация случайной длины строки от 5 до 19
                string line = GenerateRandomString(length);
                writer.WriteLine(line);
            }
        }
    }

    private static string GenerateRandomString(int length)
    {
        const string chars = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФЧЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюяABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] stringChars = new char[length];
        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[rand.Next(chars.Length)];
        }
        return new string(stringChars);
    }

    private static void FilterAndWriteToFile()
    {
        using (StreamReader reader = new StreamReader("input.txt"))
        using (StreamWriter writer = new StreamWriter("output.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!line.Any(c => c >= 'А' && c <= 'я'))
                {
                    writer.WriteLine(line);
                }
            }
        }
    }
}
