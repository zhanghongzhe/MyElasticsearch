using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyElasticsearch
{
    class Program
    {
        static void Main(string[] args)
        {
            //单个节点
            var settings1 = new Elasticsearch.Net.ConnectionConfiguration(new Uri("http://localhost:9200")).RequestTimeout(TimeSpan.FromMinutes(2));
            var lowlevelClient1 = new Elasticsearch.Net.ElasticLowLevelClient(settings1);

            //连接池：Connection pool
            var uris = new[]
            {
                new Uri("http://localhost:9200"),
                new Uri("http://localhost:9201"),
                new Uri("http://localhost:9202"),
            };
            var connectionPool = new Elasticsearch.Net.SniffingConnectionPool(uris);
            var settings = new Elasticsearch.Net.ConnectionConfiguration(connectionPool);
            var lowlevelClient = new Elasticsearch.Net.ElasticLowLevelClient(settings);
        }
    }
}
