using System;

namespace Test11._2
{
    interface I1
    { void M(); }
    interface I2
    { void M(); }

    public class A : I1, I2
    {
        public static void M()
        {
            Console.WriteLine("A.M()");
        }
        void I2.M()
        {
            Console.WriteLine("I2.M()");
        }
        void I1.M()
        {
            Console.WriteLine("I1.M()");
        }
    }
    class B : A, I1, I2
    {
        void I1.M()
        {
            Console.WriteLine("I1B.M()");
        }
        void I2.M()
        {
            Console.WriteLine("I2B.M()");
        }
    }
}
