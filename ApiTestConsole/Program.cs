using System;
using TuDelft.Api;

namespace ApiTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("What is yourname?");
            string name = Console.ReadLine();
            Console.WriteLine($"Welcome {name}!");


            var items = TuDelftWorkspace.Get();
            foreach(var item in items)
            {
                Console.WriteLine($"{item.Location} - {item.NumberOfAvailableComputers}");
            }

            Console.ReadLine();

        }
    }
}
