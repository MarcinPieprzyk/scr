using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pieprzyk_Przekop
{
    class BankAgent : Agent
    {
        public double balance { get; set; }
        private Action<double> action { get; }
        private Action actionSubAgent;
        public Mutex mutex { get; }
        public object o { get; }
        private ConcurrentQueue<Action> actions { get; set; }
        public ConcurrentDictionary<int, double> pairs { get; set; }
        private StreamWriter streamWriter { get; }
        public BankAgent(double balance, Action<double> action, int Id) : base(Id)
        {
            this.balance = balance;
            this.action = action;
            mutex = new Mutex();
            o = new object();
            actions = new ConcurrentQueue<Action>();
            pairs = new ConcurrentDictionary<int, double>();
            /*
            streamWriter = new StreamWriter(@"path");
            streamWriter.AutoFlush = true;
            streamWriter.WriteLine($"{DateTime.Now} saldo: {balance}");
            */
        }
        public override void Update()
        {
            action(balance);
            Thread.Sleep(500);

            //ConcurrentQueue
            if (actions.TryDequeue(out actionSubAgent))
                actionSubAgent();
        }
        public void Enqueue(Action action)
        {
            actions.Enqueue(action);
        }
    }
}
