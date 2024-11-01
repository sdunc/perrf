using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static ulong SumOfFactors(ulong number)
    {
        ulong sum = 1;
        ulong root = (ulong)Math.Sqrt(number);

        for (ulong i = 2; i <= root; i++)
        {
            if (number % i == 0)
            {
                sum += i;
                sum += number / i;
            }
        }

        return sum;
    }

    static void Main()
    {
        Console.WriteLine("Naive loop");
        Stopwatch sw = Stopwatch.StartNew();

        for (ulong number = 2; number <= ulong.MaxValue; number++)
        {
            if (number == SumOfFactors(number))
            {
                Console.WriteLine($"{number} ({sw.Elapsed})");
            }

            if (sw.Elapsed > TimeSpan.FromSeconds(5))
            {
                Console.WriteLine("Cannot finish in 5 seconds!\n");
                break;
            }
        }

        sw.Stop();

        Console.WriteLine("Smart loop");
        sw = Stopwatch.StartNew();

        for (int p = 2; p < 32; p++)
        {
            ulong x = (ulong)(((1 << p) - 1) << (p - 1));
            ulong sum = 1;

            for (ulong n = 2; n < ulong.MaxValue; n++)
            {
                if (x % n == 0)
                {
                    var div = x / n;
                    if (div > n)
                    {
                        sum += div;
                        sum += n;
                    }
                    else if (div == n)
                    {
                        sum += n;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (sw.Elapsed > TimeSpan.FromSeconds(10))
            {
                Console.WriteLine("Cannot finish in 5 seconds!\n");
                break;
            }


            if (sum == x)
            {
                Console.WriteLine($"{x} ({sw.Elapsed})");
            }
        }

        Console.WriteLine("primes");
        sw = Stopwatch.StartNew();
        int[] primes = [2, 3, 5, 7, 13, 17, 19, 31]; // Known primes that make 64-bit perfect numbers
        foreach (int p in primes)
        {
            ulong mersennePrime = (ulong)((1UL << p) - 1);
            ulong perfect = (ulong)(1UL << (p - 1)) * mersennePrime;
            Console.WriteLine($"{perfect} ({sw.Elapsed})");
        }

        sw.Stop();

        Console.Read();
    }
}
