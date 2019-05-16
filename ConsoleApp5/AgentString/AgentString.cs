using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pieprzyk_Przekop
{
    class AgentString : Agent
    {
        public Dictionary<string, int> pairs { get; set; }
        private List<string> list { get; set; }
        private int steps { get; }
        public AgentString(List<string> list, int steps, int Id) : base(Id)
        {
            this.list = list;
            pairs = new Dictionary<string, int>();
            this.steps = steps;
        }
        public override void Update()
        {
            int count = list.Count;
            for (int j = 0; j < steps; ++j)
            {
                List<string> subList;
                if (j + 1 == steps)
                    subList = list.GetRange(0, list.Count);
                else
                {
                    subList = list.GetRange(0, count / steps);
                    list.RemoveRange(0, count / steps);
                }

                foreach (var i in subList)
                {
                    if (pairs.ContainsKey(i))
                        pairs[i] += 1;
                    else
                        pairs.Add(i, 1);
                }
            }
            Console.WriteLine($"{Id} has ended up!");
            HasFinished = true;
        }
    }
}
