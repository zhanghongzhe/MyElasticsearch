using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyElasticSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var c1 = new Class2();
            //c1.Index1();
            c1.Analyze1();
            Console.WriteLine("Complete");
            Console.ReadLine();
        }
    }
}
