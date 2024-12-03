using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            { 
                bool A = double.TryParse(Console.ReadLine(), out double a);
                bool B = double.TryParse(Console.ReadLine(), out double b);

                if (!A || !B)
                    throw new Exception("Это не число");
                if (a>255 || b>255 ||a<0 ||b<0 )
                    throw new Exception("Число не попало в диапазон");
                if (b == 0.0)
                    throw new Exception("Деление на ноль");

                Console.WriteLine("a / b = {a / b}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }
    }
}
