using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pieprzyk_Przekop
{
    class WorkStealingAgent : Agent
    {
        public ConcurrentQueue<Action<WorkStealingAgent>> Queue { get; set; }
        private Action<WorkStealingAgent> action;
        public WorkStealingAgent(int Id) : base(Id)
        {
            Queue = new ConcurrentQueue<Action<WorkStealingAgent>>();
        }
        public override void Update()
        {
            WorkStealingAgent s = new WorkStealingAgent(0);
            while(!Queue.IsEmpty)
            {
                Queue.TryDequeue(out action);
                action(s);
                return;
            }
            
            HasFinished = true;
        }
    }
}
