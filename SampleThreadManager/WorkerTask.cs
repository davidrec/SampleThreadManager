using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace SampleThreadManager
{
    class WorkerTask
    {
        int timeout = 10000;
        DateTime startTime = DateTime.Now;
        int numbytes = 999999999;
        Manager manager;

        public WorkerTask(Manager _manager)
        {
            manager = _manager;
            Console.WriteLine("WorkerTask ctor() success. | thread id:{1}!", numbytes, Thread.CurrentThread.ManagedThreadId);

        }

        ~WorkerTask()
        {
            Console.WriteLine("WorkerTask Dtor() success. | thread id:{1}!", numbytes, Thread.CurrentThread.ManagedThreadId);
        }

        public void DoWork(CancellationToken token)
        {
            
            int iteretion_counter = 0;
            Console.WriteLine("WorkerTask DoWork() | thread id:{1}!", numbytes, Thread.CurrentThread.ManagedThreadId);
            while (!token.IsCancellationRequested )
            {
                if (isTimeOut())
                {
                    Console.WriteLine("WorkerTask DoWork()| ending function. | iteretion_counter ={0} | thread id:{1}!", iteretion_counter, Thread.CurrentThread.ManagedThreadId);
                    manager.CreateNewWorker();
                    return;
                }

                iteretion_counter++;
                Console.WriteLine("WorkerTask DoWork() |iteretion_counter ={0} | thread id:{1}!", iteretion_counter, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(Program.sleeping_time);
            }
        }

        private bool isTimeOut()
        {
            if (DateTime.Now.Subtract(startTime).TotalMilliseconds > timeout)
            {
                Console.WriteLine("Timeout, startTime =[{0}], timeOut = [{1}], Now=  [{2}], Milisec passed = [{3}]", startTime, timeout, DateTime.Now, DateTime.Now.Subtract(startTime).TotalMilliseconds);
                return true;
            }
            return false;
        }
    }
}
