using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pieprzyk_Przekop
{
    public class AgentSplit : Agent
    {
        public List<string> words { get; set; }
        private string text { get; }
        public AgentSplit(int Id, string text) : base(Id)
        {
            this.text = text.ToLower();
        }
        public override void Update()
        {
            words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            Console.WriteLine($"{Id} has ended up!");
            HasFinished = true;
        }
    }
}
