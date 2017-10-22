using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyElasticsearch.ElasticsearchNET
{
    /// <summary>
    /// Elasticsearch .NET只能返回Json类型，使用<byte[]>或者<string>作为返回值
    /// </summary>
    public class Class1
    {
        public void Test()
        {
            this.Get1();
        }
        public void Test1()
        {
            //单个节点
            var settings1 = new Elasticsearch.Net.ConnectionConfiguration(new Uri("http://localhost:9200")).RequestTimeout(TimeSpan.FromMinutes(2));
            var lowlevelClient1 = new Elasticsearch.Net.ElasticLowLevelClient(settings1);
        }
        public void Test2()
        {
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
        #region Index
        public void Index1()
        {
            var settings = new Elasticsearch.Net.ConnectionConfiguration(new Uri("http://localhost:9200")).RequestTimeout(TimeSpan.FromMinutes(2));
            var lowlevelClient = new Elasticsearch.Net.ElasticLowLevelClient(settings);

            var person = new Person { FirstName = "Martijn", LastName = "Laarman" };
            //synchronous method that returns an IIndexResponse
            var indexResponse = lowlevelClient.Index<byte[]>("people", "person", "1", person);
            byte[] responseBytes = indexResponse.Body;

            //asynchronous method that returns a Task<IIndexResponse> that can be awaited
            //var asyncIndexResponse = lowlevelClient.IndexAsync<string>("people", "person", "1", person);
            //string responseString = asyncIndexResponse.Body;
        }
        public void Index2()
        {
            var people = new object[]
            {
                new { index = new { _index = "people", _type = "person", _id = "1"  }},
                new { FirstName = "Martijn", LastName = "Laarman" },
                new { index = new { _index = "people", _type = "person", _id = "2"  }},
                new { FirstName = "Greg", LastName = "Marzouka" },
                new { index = new { _index = "people", _type = "person", _id = "3"  }},
                new { FirstName = "Russ", LastName = "Cam" },
            };

            var settings = new Elasticsearch.Net.ConnectionConfiguration(new Uri("http://localhost:9200")).RequestTimeout(TimeSpan.FromMinutes(2));
            var lowlevelClient = new Elasticsearch.Net.ElasticLowLevelClient(settings);
            var indexResponse = lowlevelClient.Bulk<Stream>(people);
            Stream responseStream = indexResponse.Body;
        }
        #endregion
        #region Get
        public void Get1()
        {
            var settings = new Elasticsearch.Net.ConnectionConfiguration(new Uri("http://localhost:9200")).RequestTimeout(TimeSpan.FromMinutes(2));
            var lowlevelClient = new Elasticsearch.Net.ElasticLowLevelClient(settings);
            var responseJson = lowlevelClient.Get<string>("people", "person", "1").Body;
        }
        #endregion
        #region Search
        public void Search1()
        {
            var settings = new Elasticsearch.Net.ConnectionConfiguration(new Uri("http://localhost:9200")).RequestTimeout(TimeSpan.FromMinutes(2));
            var lowlevelClient = new Elasticsearch.Net.ElasticLowLevelClient(settings);
            var searchResponse = lowlevelClient.Search<string>("people", "person", new
            {
                from = 0,
                size = 10,
                query = new
                {
                    match = new
                    {
                        field = "firstName",
                        query = "Martijn"
                    }
                }
            });

            var responseJson = searchResponse.Body;
        }
        #endregion
        #region Delete
        public void Delete1()
        {
            var settings = new Elasticsearch.Net.ConnectionConfiguration(new Uri("http://localhost:9200")).RequestTimeout(TimeSpan.FromMinutes(2));
            var lowlevelClient = new Elasticsearch.Net.ElasticLowLevelClient(settings);
            var p = lowlevelClient.Delete<byte[]>("people", "person", "1");
        }
        #endregion
        #region Model
        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        #endregion
    }
}
