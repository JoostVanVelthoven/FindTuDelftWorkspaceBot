using System;
using System.Linq;
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

            Console.WriteLine("What is your building of choice?");
            string bulding = Console.ReadLine();

            Console.WriteLine("How many computers do you need?");
            int requestedComputers = Convert.ToInt32(Console.ReadLine());

            //TU rest api will be managed by this code.

            //TODO add filtering

            var items = TuDelftWorkspace.Get()                                            
                       .ToList();
                
            foreach(var item in items)
            {
                Console.WriteLine($"{item.Location} - {item.NumberOfAvailableComputers}");
            }

            if(!items.Any())
            {
                Console.WriteLine("No places found");
            }
            Console.ReadLine();

        }
    }
}
