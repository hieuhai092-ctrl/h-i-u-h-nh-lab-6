using System;
using System.Threading;

internal class Program
{
    private static int x = 3;

    // Khai bao 3 Semaphore
    static Semaphore sem1 = new Semaphore(0, 2);
    static Semaphore sem2 = new Semaphore(0, 1);
    static Semaphore sem3 = new Semaphore(1, 1);

    static void Main(string[] args)
    {
        Thread t1 = new Thread(Add);
        Thread t2 = new Thread(Subtract);
        Thread t3 = new Thread(Multiply);
        Thread t4 = new Thread(Divide);
        Thread t5 = new Thread(Power);

        // Khoi dong nguoc thu tu
        t5.Start();
        t4.Start();
        t3.Start();
        t2.Start();
        t1.Start();

        t1.Join();
        t2.Join();
        t3.Join();
        t4.Join();
        t5.Join();

        Console.WriteLine("Final value of x: " + x);
        Console.ReadLine();
    }

    static void Add()
    {
        x = x + 3;
        Console.WriteLine("Add: " + x);

        sem1.Release();
    }

    static void Subtract()
    {
        x = x - 1;
        Console.WriteLine("Subtract: " + x);

        sem1.Release();
    }

    static void Multiply()
    {
        sem1.WaitOne();
        sem1.WaitOne();

        x = x * 4;
        Console.WriteLine("Multiply: " + x);

        sem2.Release();
    }

    static void Divide()
    {
        sem2.WaitOne();

        x = x / 2;
        Console.WriteLine("Divide: " + x);

        sem3.Release();
    }

    static void Power()
    {
        sem3.WaitOne();

        x = (int)Math.Pow(x, 2);
        Console.WriteLine("Power: " + x);
    }
}
