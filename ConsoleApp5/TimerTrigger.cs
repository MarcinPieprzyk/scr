using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pieprzyk_Przekop
{
    class TimerTrigger
    {
        private List<IRunnable> runnables { get; set; }
        private IEnumerable<IEnumerator<float>> enumerators { get; set; }
        private List<int> samplingTimes { get; set; }
        private Timer timer { get; set; }
        private object o;
        private int x = 0;
        public TimerTrigger(List<IRunnable> runnables)
        {
            this.runnables = runnables;
            enumerators = runnables.Select(i => i.CoroutineUpdate()).ToList();
            samplingTimes = new List<int>();
            
            timer = new Timer(new TimerCallback(timerCallback), o, 25, 25);
            
        }
        private void timerCallback(object o)
        {
            for (int i = 0; i < samplingTimes.Count; i++)
            {
                if (samplingTimes[i] == i)
                {
                    enumerators.ElementAt(i).MoveNext();
                    samplingTimes[i] = 0;
                }
            }
            samplingTimes.ForEach(i => ++i);
        }
    }
}
