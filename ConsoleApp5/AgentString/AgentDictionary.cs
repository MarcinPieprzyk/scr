using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pieprzyk_Przekop
{
    class AgentDictionary : Agent
    {
        public Dictionary<string, int> pairs { get; set; }
        private List<Dictionary<string, int>> dictionaries { get; }
        public AgentDictionary(int Id, List<Dictionary<string, int>> dictionaries) : base(Id)
        {
            pairs = new Dictionary<string, int>();
            this.dictionaries = dictionaries;
        }
        public override void Update()
        {
            foreach (var dictionary in dictionaries)
                foreach (var i in dictionary)
                {
                    if (pairs.ContainsKey(i.Key))
                        pairs[i.Key] += i.Value;
                    else
                        pairs.Add(i.Key, i.Value);
                }

            SortAndWriteToConsole(pairs.ToList());
            Console.WriteLine($"{Id} has ended up!");
            HasFinished = true;
        }
        public System.Collections.Generic.IEnumerator<int> GetEnumerator()
        {

            yield return default(int);
        }
        private void SortAndWriteToConsole(List<KeyValuePair<string, int>> myList)
        {
            myList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            var keyValues = new List<List<KeyValuePair<string, int>>>();
            while (myList.Count != 0)
            {
                int index = myList.FindLastIndex(i => i.Value == myList[0].Value) + 1;
                keyValues.Add(myList.GetRange(0, index));
                myList.RemoveRange(0, index);
            }

            foreach (var i in keyValues)
                i.Sort((pair1, pair2) => pair1.Key.CompareTo(pair2.Key));

            foreach (var i in keyValues)
                foreach (var j in i)
                    Console.WriteLine($"{j.Key} - {j.Value}");
        }
    }
}
