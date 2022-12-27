using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    #region Semaphore
    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        string name = "SEMAPHORE";
    //        Semaphore semaphore = new Semaphore(3, 7, name);
    //        for (int i = 0; i < 10; i++)
    //        {
    //            ThreadPool.QueueUserWorkItem(SomeMethod, semaphore);
    //        }
    //        Console.ReadLine();
    //    }
    //    private static void SomeMethod(object state)
    //    {
    //        var s = state as Semaphore;
    //        bool st = false;

    //        while (!st)
    //        {
    //            if (s.WaitOne(500))
    //            {
    //                try
    //                {
    //                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} got the key");
    //                    Thread.Sleep(2000);
    //                }
    //                finally
    //                {
    //                    st = true;
    //                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} returned the key");
    //                    s.Release();
    //                }
    //            }
    //            else
    //            {
    //                s.Release(2);
    //                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}, we do not have enough keys. Please, wait.");
    //            }
    //        }
    //    }
    //}
    #endregion

    #region Semaphore Slim
    class Program
    {
        static SemaphoreSlim _semaSlim = new SemaphoreSlim(4);  
        static void Main(string[] args)
        {
            for (int i = 0; i < 6; i++)
            {
                var name = $"Thread {i}";
                int seconds = 2 + 2 * i;
                var t = new Thread(() =>
                {
                    AccessDatabase(name, seconds);
                });
                t.Start();
            }

        }

        private static void AccessDatabase(string name, int seconds)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{name} waits for access");
            _semaSlim.Wait();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{name} is working on database");
            Thread.Sleep(seconds * 1000);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{name} completed its work");
        }
    }
    #endregion
}
