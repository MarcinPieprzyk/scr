using System;

namespace Pieprzyk_Przekop
{
    public class SineGeneratingAgent : Agent
    {
        public SineGeneratingAgent(int id) : base(id) { }
        public double Output { get; set; }
        public DateTime time { get; set; }

        public new void Run()
        {
            time = DateTime.UtcNow;
            base.Run();
        }
        public override void Update()
        {
            Output = Math.Sin(time.Millisecond);
            if (Convert.ToInt32((DateTime.UtcNow - time).Milliseconds / 100) >= Id % 10)
            {
                Console.WriteLine($"{Id} has ended up!");
                HasFinished = true;
            }
            Console.WriteLine($"{Id} is working!");
        }
    }
}
