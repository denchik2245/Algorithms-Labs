using System;
class Program
{
    static void Main()
    {
        int a = int.Parse(Console.ReadLine());

        int b = int.Parse(Console.ReadLine());

        int nod = 0;

        while (a != b)
        {
            if (a > b)
            {
                a = a - b;
            }
            else
            {
                b = b - a;
            }
        }

        nod = a;
        Console.WriteLine("НОД: " + nod);
    }
}
