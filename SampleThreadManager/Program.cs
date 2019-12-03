using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleThreadManager
{
    class Program
    {

        public static int sleeping_time = 1000;
        static void Main(string[] args)
        {
            Console.WriteLine("Start Main!");
            Console.WriteLine("Main | thread id:{0}!", Thread.CurrentThread.ManagedThreadId);
            Manager manager;
            CancellationTokenSource cancellation_token_source;

            cancellation_token_source = new CancellationTokenSource();
            manager = new Manager();

            Console.WriteLine("Main | start task manager");
            Task managerTask = Task.Run(() =>
            {
                Console.WriteLine("Main | init manager");
                manager.Init();
                Console.WriteLine("Main | run manager");
                manager.Run(cancellation_token_source);
            });
            managerTask.Wait();
            Console.WriteLine("Main | finish.");
        }
    }
}
