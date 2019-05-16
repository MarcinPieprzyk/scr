using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pieprzyk_Przekop
{
    public static class MainMethods
    {
        #region Basic
        public static void GenerateRunnablesBasic(List<IRunnable> runnables, int agents = 100)
        {
            int constantAgentId = 0;
            int agentId = 0;
            int sineAgentId = 0;
            for (int i = 0; i < agents; ++i)
            {
                if (i % 3 == 0)
                    runnables.Add(new ConstantCountingAgent(constantAgentId++));
                else if (i % 3 == 1)
                    runnables.Add(new CountingAgent(agentId++));
                else
                    runnables.Add(new SineGeneratingAgent(sineAgentId++));
            }
        }
        public static void RunThreadsBasic(List<IRunnable> runnables)
        {
            var threads = new List<Thread>();
            foreach (var i in runnables)
                threads.Add(new Thread(() =>
                {
                    if (i is SineGeneratingAgent)
                        (i as SineGeneratingAgent).Run();
                    else
                        i.Run();
                }));
            
            foreach (var i in threads)
                i.Start();
            
            while (runnables.Any(i => !i.HasFinished)) ;
        }
        #endregion

        #region SumInts
        public static void GenerateRunnablesSumInts(List<IRunnable> runnables, int agents = 4, int steps = 2)
        {
            var randoms = new List<int>();
            var r = new Random();
            int size = 10000;
            for (int i = 0; i < size; ++i)
                randoms.Add(r.Next(10, 10000));
                //randoms.Add(10);

            for (int i = 0; i < agents; ++i)
            {
                if (i + 1 == agents)
                    runnables.Add(new AgentSum(randoms.GetRange(0, randoms.Count), steps, i));
                else
                    runnables.Add(new AgentSum(randoms.GetRange(0, size / agents), steps, i));
                randoms.RemoveRange(0, size / agents);
            }
        }
        public static void RunThreadsSumInts(List<IRunnable> runnables)
        {
            var threads = new List<Thread>();
            foreach (var i in runnables)
                threads.Add(new Thread(() => i.Run()));

            foreach (var i in threads)
                i.Start();

            while (threads.Any(i => i.ThreadState != ThreadState.Stopped)) ;
            var list = new List<int>();
            foreach (var i in runnables)
                list.Add((i as AgentSum).Value);

            var agent = new AgentSum(list, 1, runnables.Count);
            var thread = new Thread(() => agent.Run());
            thread.Start();
            while (thread.ThreadState != ThreadState.Stopped) ;
            Console.WriteLine(agent.Value);
        }
        #endregion

        #region String
        public static void GenerateRunnablesStringCounter(List<IRunnable> runnables, int agents = 4, int steps = 2)
        {
            using (StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "jakistekst.txt", Encoding.Unicode))
            {
                runnables.Add(new AgentSplit(0, file.ReadToEnd()));
            }
            RunThreadsString(runnables);
            //RunFibersString(runnables);

            GenerateAgentsString(runnables, steps, agents);
            RunThreadsString(runnables.GetRange(1, runnables.Count - 1));
            //RunFibersString(runnables.GetRange(1, runnables.Count - 1));

            var dictionaries = new List<Dictionary<string, int>>();
            for (int i = 1; i <= agents; ++i)
                dictionaries.Add((runnables[i] as AgentString).pairs);
            runnables.Add(new AgentDictionary(runnables.Count, dictionaries));

            RunThreadsString(runnables.GetRange(runnables.Count - 1, 1));
            //RunFibersString(runnables.GetRange(runnables.Count - 1, 1));
        }
        static void GenerateAgentsString(List<IRunnable> runnables, int steps, int agents)
        {
            AgentSplit agent = runnables[0] as AgentSplit;
            var count = agent.words.Count;
            for (int i = 0; i < agents; ++i)
            {
                List<string> subList;
                if (i + 1 == agents)
                    subList = agent.words.GetRange(0, agent.words.Count);
                else
                {
                    subList = agent.words.GetRange(0, count / agents);
                    agent.words.RemoveRange(0, count / agents);
                }
                runnables.Add(new AgentString(subList, steps, i + 1));
            }
        }
        private static void RunThreadsString(List<IRunnable> runnables)
        {
            runnables.ForEach(i => new Thread(() => i.Run()).Start());
            while (runnables.Any(i => !i.HasFinished)) ;
        }
        private static void RunFibersString(List<IRunnable> runnables)
        {
            var enumerators = runnables.Select(i => i.CoroutineUpdate()).ToList();
            runnables.ForEach(i => i.HasFinished = false);
            while (runnables.Any(i => !i.HasFinished))
            {
                enumerators.ForEach(i => i.MoveNext());
                Thread.Sleep(100);
            }
            Console.WriteLine("Fibers have ended up!");
        }
        #endregion

        #region Bank
        public static void GenerateRunnablesBank(List<IRunnable> runnables)
        {
            var baseBalance = 100.0;
            BankAgent bank = new BankAgent(baseBalance, (x) => Console.WriteLine(Math.Round(x, 4)), 0);
            runnables.Add(bank);
            runnables.Add(new SubBankAgent(bank, (y) => { return y - 1.2; }, 0));
            runnables.Add(new SubBankAgent(bank, (y) => { return y + 0.33; }, 1));
            runnables.Add(new SubBankAgent(bank, (y) => { return y * 1.07; }, 2));
            runnables.Add(new SubBankAgent(bank, (y) => { return y + Math.Sin(DateTime.UtcNow.Millisecond); }, 3));
            runnables.Add(new SubBankAgent(bank, (y) => { return y - Math.PI; }, 4));
            /*runnables.AddRange(runnables.GetRange(1, 5));
            runnables.AddRange(runnables.GetRange(1, 5));
            runnables.AddRange(runnables.GetRange(1, 5));
            runnables.AddRange(runnables.GetRange(1, 5));
            runnables.AddRange(runnables.GetRange(1, 5));*/
        }
        public static void RunThreadsBank(List<IRunnable> runnables)
        {
            var threads = new List<Thread>();
            runnables.ForEach(i => threads.Add(new Thread(() => i.Run())));
            threads.ForEach(i => i.Start());
            while (threads.Any(i => i.ThreadState != ThreadState.Stopped)) ;
        }
        public static void Lamport(int size = 20)
        {
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < size; ++i)
            {
                int id = i;
                threads.Add(new Thread(() =>
                {
                    for (int j = 0; j < id; ++j)
                        while (threads[j].ThreadState != ThreadState.Stopped) ;
                    Thread.Sleep(100);
                    Console.WriteLine($"{id+1} has ended up!");
                }));
            }
            threads.ForEach(i => i.Start());
        }
        #endregion

        #region test
        public static void GenerateRunnablestest(List<IRunnable> runnables, int agents = 10)
        {
            for (int i = 0; i < agents; ++i)
            {
                runnables.Add(new test(i));
            }
        }
        #endregion
    }
}
