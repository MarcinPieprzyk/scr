using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pieprzyk_Przekop
{
    class AgentSum : Agent
    {
        private int steps { get; }
        private List<int> list { get; }
        public AgentSum(List<int> list, int steps, int id) : base(id)
        {
            this.list = list;
            this.steps = steps;
        }
        public override void Update()
        {
            List<int> subSums = new List<int>();
            int count = list.Count;
            for (int i = 0; i < steps; ++i)
            {
                int summ = list.GetRange(0, count / steps).Sum();
                subSums.Add(summ);
                list.RemoveRange(0, count / steps);
            }
            if (list.Count != 0) subSums.Add(list.Sum());
            Value = subSums.Sum();
            Console.WriteLine($"{Id} has ended up!");
            HasFinished = true;
        }
    }
}
