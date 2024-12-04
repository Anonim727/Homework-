using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args);


        public class Fraction
        {
            // Числитель дроби
            public int Numerator { get; set; }
            // Знаменатель дроби
            public int Denominator { get; set; }

            public Fraction(int numerator, int denominator)
            {
                Numerator = numerator;
                Denominator = denominator;
            }

            public override string ToString()
            {
                return $"{Numerator}/{Denominator}";
            }
        }
        public static Fraction operator *(Fraction a, Fraction b)
        {
            Fraction multiplication = new Fraction();
            multiplication.Numerator = a.Numerator * b.Numerator;
            multiplication.Denominator = a.Denominator * b.Denominator;
            return multiplication;
        }

    }
}
        

        
    

