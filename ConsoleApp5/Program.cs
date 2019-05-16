using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Pieprzyk_Przekop
{
    class Program
    {
        static void GenerateRunnables(List<IRunnable> runnables)
        {
            //MainMethods.GenerateRunnablesBasic(runnables /*agents*/);

            //MainMethods.GenerateRunnablesSumInts(runnables /*agents*/ /*steps*/);

            //MainMethods.GenerateRunnablesStringCounter(runnables /*agents*/ /*steps*/);

            //MainMethods.GenerateRunnablesBank(runnables);
            MainMethods.GenerateRunnablestest(runnables);
        }
        static void RunThreads(List<IRunnable> runnables)
        {
            //MainMethods.RunThreadsBasic(runnables);

            //MainMethods.RunThreadsSumInts(runnables);

            MainMethods.RunThreadsBank(runnables);

            Console.WriteLine("Threads have ended up!");
        }
        static void RunFibers(List<IRunnable> runnables)
        {
            var enumerators = runnables.Select(i => i.CoroutineUpdate()).ToList();
            runnables.ForEach(i => i.HasFinished = false);
            while (runnables.Any(i => !i.HasFinished))
            {
                enumerators.ForEach(i => i.MoveNext());
                Thread.Sleep(100);
            }
            Console.WriteLine("Fibers have ended up!");
        }
        static void Main(string[] args)
        {
            var runnables = new List<IRunnable>();
            GenerateRunnables(runnables);
            //RunThreads(runnables);
            //Console.ReadLine();
            //RunFibers(runnables);
            //MainMethods.Lamport();
            new TimerTrigger(runnables);
            Console.ReadLine();
        }
    }
}
