using System.Collections.Generic;

namespace Pieprzyk_Przekop
{
    public interface IRunnable
    {
        bool HasFinished { get; set; }
        void Run();
        IEnumerator<float> CoroutineUpdate();
    }
}
