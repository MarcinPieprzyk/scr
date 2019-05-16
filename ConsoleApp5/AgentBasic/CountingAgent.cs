using System;

namespace Pieprzyk_Przekop
{
    public class CountingAgent : Agent
    {
        public CountingAgent(int id) : base(id) { }
        public override void Update()
        {
            if (++Value >= Id)
            {
                Console.WriteLine($"{Id} has ended up!");
                HasFinished = true;
            }
            Console.WriteLine($"{Id} is working!");
        }
        
    }
}
