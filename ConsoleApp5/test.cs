using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pieprzyk_Przekop
{
    class test : Agent
    {
        public test(int id) : base(id) { }
        public override void Update()
        {
            Console.WriteLine($"{Id}");
        }
    }
}
