using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        { }
        abstract class Animal
        {
            private string Name;
            private int Weight;
            private string Sound;

            public Animal(string N, int W, string S)
            {
                Name = N;
                Weight = W;
                Sound = S;
            }
            public MakeSound()
            {
                Console.WriteLine("Name: {0} , Weight: {1} , Sound: {2}", Name, Weight, Sound);
            }

            public class Cat : Animal
            {
                private string CatName;
                private int CatWeight;
                private string CatSound;
                public Animal(string Name, int Weight, string Sound)
                {
                    CatName = Name;
                    CatWeight = Weight;
                    CatSound = Sound;


                }
            }
            public class Dog : Animal
            {
                private string DogName;
                private int DogWeight;
                private string DogSound;
                public Animal(string Name, int Weight, string Sound)
                {
                    DogName = Name;
                    DogWeight = Weight;
                    DogSound = Sound;
                }
            }
        }
      
        }

    static void Main(string[] args)
    {
        Dog animal1 = new Dog("Барсик", 37, "gav");
        Cat animal2 = new Cat("Мурзик", 6), "meow";
    }