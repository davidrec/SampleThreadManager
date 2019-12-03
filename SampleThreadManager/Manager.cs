using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleThreadManager
{
    class Manager
    {
        List<Task> task_pool;
        int thread_num;

        CancellationTokenSource cancelToken;

        public void Init()
        {
            thread_num = 2;
            task_pool = new List<Task>(thread_num);

        }

        public void Run(CancellationTokenSource token)
        {
            cancelToken = token;

            for (int i = 0; i < thread_num; i++)
            {
                CreateNewWorker();
            }


            while (!cancelToken.IsCancellationRequested)
            {
                Console.WriteLine("Manager Run() While loop | thread id:{0}!", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(Program.sleeping_time);
            }
        }

        public void CreateNewWorker()
        {
            Console.WriteLine("Manager CreateNewWorker() | thread id:{0}!", Thread.CurrentThread.ManagedThreadId);
            WorkerTask new_task = new WorkerTask(this);
            task_pool.Add(Task.Run(() => new_task.DoWork(cancelToken.Token)));
        }
    }
}
