using System;
using System.Collections.Generic;
using System.Threading;

namespace Pieprzyk_Przekop
{
    public abstract class Agent : IRunnable
    {
        public bool HasFinished { get; set; } = false;
        protected int Id { get; set; }
        public int Value { get; set; }
        private float Freq { get; set; } = 100;
        public Agent(int Id)
        {
            this.Id = Id;
            Value = 0;
        }
        public abstract void Update();
        public virtual void Run()
        {
            while(!HasFinished)
            {
                Update();
                Thread.Sleep(Convert.ToInt32(1000.0f / Freq));
            }
        }
        public IEnumerator<float> CoroutineUpdate()
        {
            while(!HasFinished)
            {
                Update();
                yield return 1.0f;
            }
            yield break;
        }
    }
}
