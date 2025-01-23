using System;
using System.Globalization;

public class QuadraticEquation
{
    // поля
    private double A, B, C;
    
    // конструктор
    public QuadraticEquation(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
    }
    
    // метод для вычисления корней квадратного уравнения
    public double[] FindRoots()
    {
        double discriminant = B * B - 4 * A * C;

        if (discriminant > 0)
        {
            double root1 = (-B + Math.Sqrt(discriminant)) / (2 * A);
            double root2 = (-B - Math.Sqrt(discriminant)) / (2 * A);
            return new double[] { root1, root2 }; // возврат массива из двух корней
        }
        else if (discriminant == 0)
        {
            double root = -B / (2 * A);
            return new double[] { root }; // возврат массива из одного корня
        }
        else
        {
            return new double[] { }; // возврат пустого массива
        }
    }

    // перегрузка метода ToString()
    public override string ToString()
    {
        return $"Уравнение: {A}x^2 + {B}x + {C} = 0";
    }
    
    //Унарные операции:
    // операция увиличения коэфициентов на 1
    public static QuadraticEquation operator ++(QuadraticEquation eq)
    {
        eq.A++;
        eq.B++;
        eq.C++;
        return eq;
    }
    // операция уменьшения коэфициентов на 1
    public static QuadraticEquation operator --(QuadraticEquation eq)
    {
        eq.A--;
        eq.B--;
        eq.C--;
        return eq;
    }
    
    // Операции приведения типа
    // операция double - результатом является дискриминант уравнения
    public static implicit operator double(QuadraticEquation eq)
    {
        return eq.B * eq.B - 4 * eq.A * eq.C;
    }
    // операция bool - результатом является true, если корни существуют и false в противном случае;
    public static explicit operator bool(QuadraticEquation eq)
    {
        return (eq.B * eq.B - 4 * eq.A * eq.C) >= 0;
    }

    // Бинарные операции
    // операция == - уравнения равны, если равны их коэффициенты;
    public static bool operator ==(QuadraticEquation eq1, QuadraticEquation eq2)
    {
        return eq1.A == eq2.A && eq1.B == eq2.B && eq1.C == eq2.C;
    }
    // операция != - уравнения не равны, если не
    // равны их коэффициенты.
    public static bool operator !=(QuadraticEquation eq1, QuadraticEquation eq2)
    {
        return !(eq1 == eq2);
    }
}


class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите первый коэфициент (A): ");
        string A = Console.ReadLine();

        Console.Write("Введите второй коэфициент (B): ");
        string B = Console.ReadLine();

        Console.Write("Введите третий коэфициент (C): ");
        string C = Console.ReadLine();

        if (double.TryParse(A, out double x) && double.TryParse(B, out double y) && double.TryParse(C, out double z))
        {
            // тест первого задания
            Console.WriteLine();
            QuadraticEquation equation1 = new QuadraticEquation(x, y, z);
            Console.WriteLine(equation1.ToString());
            double[] roots1 = equation1.FindRoots();
            Console.WriteLine("----------------------------");
            
            // тест унарных операций
            // инкремент
            Console.WriteLine("Перед выполнением операции ++: " + equation1.ToString());
            equation1++;
            Console.WriteLine("После выполнения операции ++: " + equation1.ToString());
            Console.WriteLine("----------------------------");
            // декремент
            Console.WriteLine("Перед выполнением операции --: " + equation1.ToString());
            equation1--;
            Console.WriteLine("После выполнения операции --: " + equation1.ToString());
            Console.WriteLine("----------------------------");
            
            // тест операций приведения 
            // вычисление дискрименанта
            double Discriminant = equation1;
            Console.WriteLine("Дискременант: " + Discriminant);
            Console.WriteLine("----------------------------");
            // проверка на наличие корней уравнения
            bool Roots = (bool)equation1;
            Console.WriteLine("У этого уравнения есть корни: " + Roots);
            Console.WriteLine("----------------------------");
            
            // тест бинарных операций
            // операция ==
            QuadraticEquation equation2 = new QuadraticEquation(x, y, z);
            Console.WriteLine(equation1 + " == " + equation2 + " : " + (equation1 == equation2));
            Console.WriteLine("----------------------------");
            // операция != 
            Console.WriteLine(equation1 + " != " + equation2 + " : " + (equation1 != equation2));
        }
        else
        {
            Console.WriteLine("Введены некорректные данные");
        }
    }
}
