using System;
using System.IO;
using System.Collections.Generic;

/* Папкин Игорь
 * Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата. 
 * а) Сделать меню с различными функциями и представить пользователю выбор, для какой функции и на каком отрезке находить минимум. 
 * Использовать массив (или список) делегатов, в котором хранятся различные функции.
 * б) *Переделать функцию Load, чтобы она возвращала массив считанных значений. Пусть она возвращает минимум через параметр (с использованием модификатора out). */

namespace Lesson6
{
    class Program
    {
        public delegate double MathFunc(double x);

        public static double Quadratic(double x)
        {
            return Math.Sqrt(x);
        }

        public static double SinusX(double x)
        {
            return Math.Sin(x);
        }

        public static double SomeF(double x)
        {
            return x * x - 50 * x + 10;
        }

        public static void SaveFunc(string fileName, int a, int b, double h, List<MathFunc> F, byte funcNumber)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            double x = a;
            while (x <= b)
            {
                if (funcNumber == 1)
                {
                    bw.Write(F[0](x));
                    x += h;
                }

                if (funcNumber == 2)
                {
                    bw.Write(F[1](x));
                    x += h;
                }

                if (funcNumber == 3)
                {
                    bw.Write(F[2](x));
                    x += h;
                }
            }

            bw.Close();
            fs.Close();
        }
        public static double[] Load(string fileName, ref double minValue)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);

            minValue = double.MaxValue;
            double[] binaryArray = new double[fs.Length / sizeof(double)];
            double d;

            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                //Считываем значение и переходим к следующему
                binaryArray[i] = d = bw.ReadDouble();
                if (d < minValue) minValue = d;
            }

            bw.Close();
            fs.Close();

            return binaryArray;
        }

        static void Main()
        { 
            MathFunc Func1 = Quadratic;
            MathFunc Func2 = SinusX;
            MathFunc Func3 = SomeF;
            List<MathFunc> FunctionList = new List<MathFunc>();
            
            FunctionList.Add(Func1);
            FunctionList.Add(Func2);
            FunctionList.Add(Func3);

            byte funcNumber = 1;
            int a = 0;
            int b = 0;
            byte step = 0;

            //Меню----------------------------------------------
            bool condition = false;
            do
            {
                if(step == 0)
                {
                    Console.WriteLine("Выберите функцию для нахождения ее минимума: \"1\" - Math.Sqrt(x) | \"2\" - Math.Sin(x) | \"3\" - SomeF(x)");
                    if (!Byte.TryParse(Console.ReadLine(), out funcNumber) || (funcNumber > 3 || funcNumber < 1))
                    {
                        Console.Clear();
                        continue;
                    }
                    else { step++; }
                }

                if (step == 1)
                {
                    Console.WriteLine("Укажите на каком отрезке находить минимум.");
                    Console.WriteLine("Введите \"a\".");
                    if (!Int32.TryParse(Console.ReadLine(), out a))
                    {
                        Console.WriteLine("Введите число");
                        Console.Clear();
                        continue;
                    }
                    else { step++; }
                }

                if (step == 2)
                {
                    Console.WriteLine("Укажите на каком отрезке находить минимум.");
                    Console.WriteLine("Введите \"b\".");
                    if (!Int32.TryParse(Console.ReadLine(), out b))
                    {
                        Console.WriteLine("Введите число");
                        Console.Clear();
                        continue;
                    }
                }

                SaveFunc("data.bin", a, b, 0.5, FunctionList, funcNumber);
                condition = true;
            }
            while (!condition);
            //---------------------------------------------------

            double minValue = 0;
            var binaryArray = Load("data.bin", ref minValue);
            
            //Вывод минимум функции
            Console.WriteLine("Минимум функции {0:f2}", minValue);

            //Вывод массива значений функции
            foreach (double number in binaryArray)
            {
                if (number != 0)
                {
                    Console.Write("{0:f2} ", number);
                }
            }

            Console.ReadKey();
        }
    }
}

