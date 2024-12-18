using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        { }
        class NegativeInteger
        {
            private int X;
            public NegativeInteger(int x)
            {
                X = x;
                if (x >= 0)
                    Console.WriteLine("Это не NegativeInteger");
                if (x < 0)
                    Console.WriteLine("Вы создали объект класса NegativeInteger");


            }
            public NegativeInteger(NegativeInteger previousNegativeInteger)
            {
                this.X = previousNegativeInteger.X;
            }
        }
        NegativeInteger A = new NegativeInteger(-5);
        NegativeInteger B = new NegativeInteger(-9);
    }
    }



