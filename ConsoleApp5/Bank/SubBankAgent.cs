using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pieprzyk_Przekop
{
    class SubBankAgent : Agent
    {
        private Func<double, double> action { get; }
        private BankAgent agent { get; set; }
        private double myBalance;
        private Mutex mutex { get; }
        private object o { get; }
        private ConcurrentQueue<Action> queue { get; set; }
        public SubBankAgent(BankAgent agent, Func<double, double> action, int Id) : base(Id)
        {
            this.action = action;
            this.agent = agent;
            myBalance = agent.balance;
            mutex = agent.mutex;
            o = agent.o;
            Random r = new Random();
            while (!agent.pairs.TryAdd(Id, r.Next(50, 150))) ;
            queue = new ConcurrentQueue<Action>(new List<Action>
            {
                 new Action(() => agent.pairs[Id] -=Convert.ToDouble(Id)*1.2),
                 new Action(() => agent.pairs[Id] +=Convert.ToDouble(Id)*0.33),
                 new Action(() => agent.pairs[Id] *=Convert.ToDouble(Id)*1.07),
                 new Action(() => agent.pairs[Id] *=Convert.ToDouble(Id)*Math.Sin(DateTime.UtcNow.Millisecond)),
                 new Action(() => agent.pairs[Id] -=Convert.ToDouble(Id)*Math.PI)
            });
        }
        public override void Update()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //mutex.WaitOne();
            //lock (o)
            //{
            //agent.balance = action(agent.balance);
            //myBalance = action(myBalance);
            //}
            //mutex.ReleaseMutex();
            //Thread.Sleep(200 * (Id + 1));


            //----------------//
            //ConcurrentQueue
            agent.Enqueue(() => { agent.balance = action(agent.balance); Console.WriteLine(Id); });
            Thread.Sleep(10 * (Id + 1));
            //----------------//


            /*
            //----------------//
            //ConcurrentQueue with ConcurrentDictionary
            Action myAction;
            if(queue.TryDequeue(out myAction))
            {
                myAction();
                queue.Enqueue(myAction);
            }
            Thread.Sleep(10 * (Id + 1));
            //----------------//
            */

            //----------------//
            //interlocked
            //nalezy zmienic wlasciwosc agent.balance na pole
            //double beginValue = agent.balance; //do sprawdzenia czy zostało zmienione przez inny watek
            //Interlocked.CompareExchange(ref agent.balance, action(agent.balance), beginValue);
            //----------------//

            //while (stopwatch.ElapsedMilliseconds < 2000) ;
            //Console.WriteLine($"{Id}: {myBalance}");
            //stopwatch.Restart();
        }
    }
}
