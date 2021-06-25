using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test11._2
{
    class Test11_2
    {
        static void Main()
        {
            A.M();
            I1 i1A = new A();
            i1A.M();
            I2 i2A = new A();
            i2A.M();
            I1 i1B = new B();
            i1B.M();
            I2 i2B = new B();
            i2B.M();
            Console.ReadLine();
        }
    }
}
