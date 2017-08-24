using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyElasticsearch
{
    /// <summary>
    /// ES有两个官方客户端：Elasticsearch .NET（低级）和NEST（高级）
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var c1 = new MyElasticsearch.NEST.Class1();
            c1.Test();
            Console.WriteLine("Complete");
            Console.ReadLine();
        }
    }
}
