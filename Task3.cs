using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;

class Program
{
    public struct Students : IComparable
    {
        public string name;
        public int age;
        public int course;

        public int CompareTo(object obj1)
        {
            Students object1;
            object1 = (Students)obj1;

            return age.CompareTo(object1.age);
        }
    }

    static int CompareTwo(Students person1, Students person2)
    {
        int check = 0;

        if(person1.course < person2.course) check = -1;

        if(person1.course > person2.course) check = 1;

        if(person1.course == person2.course)
        {
            if (person1.age < person2.age) check = -1;

            if (person1.age > person2.age) check = 1;
        }

        return check;
    }

    static void Main(string[] args)
    {
        int bakalavr = 0;
        int magistr = 0;
        int seniorCourse = 0;
        int[] statistics = new int[6];
        
        // Запомним время в начале обработки данных
        DateTime dt = DateTime.Now;
        StreamReader sr = new StreamReader("..\\..\\students_1.csv");

        Students st = new Students();
        List<Students> students = new List<Students>();

        while (!sr.EndOfStream)
        {
            try
            {
                string[] s = sr.ReadLine().Split(';');

                students.Add(st);
                st.name = s[1] + " " + s[0];
                st.age = Int32.Parse(s[5]);
                st.course = Int32.Parse(s[6]);

                if (int.Parse(s[6]) < 5) bakalavr++; else magistr++;
                if (Int32.Parse(s[6]) >= 5) seniorCourse++;

                switch (Int32.Parse(s[6]))
                {
                    case 1: if (Int32.Parse(s[5]) >= 18 && Int32.Parse(s[5]) <= 20) statistics[0]++;
                        break;
                    case 2: if (Int32.Parse(s[5]) >= 18 && Int32.Parse(s[5]) <= 20) statistics[1]++;
                        break;
                    case 3: if (Int32.Parse(s[5]) >= 18 && Int32.Parse(s[5]) <= 20) statistics[2]++;
                        break;
                    case 4: if (Int32.Parse(s[5]) >= 18 && Int32.Parse(s[5]) <= 20) statistics[3]++;
                        break;
                    case 5: if (Int32.Parse(s[5]) >= 18 && Int32.Parse(s[5]) <= 20) statistics[4]++;
                        break;
                    case 6: if (Int32.Parse(s[5]) >= 18 && Int32.Parse(s[5]) <= 20) statistics[5]++;
                        break;
                }
            }
            catch
            {
            }
        }

        sr.Close();

        Console.WriteLine("Всего студентов:{0}", students.Count);
        Console.WriteLine("Магистров:{0}", magistr);
        Console.WriteLine("Бакалавров:{0}", bakalavr);
        Console.WriteLine($"Старшекурсников:{seniorCourse}");

        //foreach (var v in list) Console.WriteLine(v);
        for(int i = 0; i < statistics.Length; i++)
        {
            Console.WriteLine($"Студентов {i+1} курса, в возрасте от 18 до 20 лет: {statistics[i]}");
        }

        //Простая сортировка по одному полю
        //for (int i = 0; i < students.Count - 1; i++)
        //{
        //    for (int j = i + 1; j < students.Count; j++)
        //    {
        //        if (students[i].course > students[j].age)
        //        {
        //            Students buffer = students[i];
        //            students[i] = students[j];
        //            students[j] = buffer;
        //        }
        //    }
        //}

        //Сортировка по одному полю, реализация интерфейса IComparable, метод CompareTo
        //students.Sort();      
        
        //Сортировка по двум полям, метод CompareTwo
        students.Sort(CompareTwo);

        foreach (Students person in students)
        {
            Console.WriteLine($"{person.course} {person.age} {person.name}");
        }

        // Вычислим время обработки данных
        Console.WriteLine(DateTime.Now - dt);
        Console.ReadKey();
    }
}

