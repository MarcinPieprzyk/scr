using System;

namespace Pieprzyk_Przekop
{
    public class ConstantCountingAgent : Agent
    {
        public ConstantCountingAgent(int id) : base(id) { }

        public override void Update()
        {
            if (++Value >= 10)
            {
                Console.WriteLine($"{Id} has ended up!");
                HasFinished = true;
            }
            Console.WriteLine($"{Id} is working!");
        }
    }
}
