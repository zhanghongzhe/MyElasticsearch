using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyElasticsearch.NEST
{
    public class Class1
    {
        private ConnectionSettings settings = null;
        private ElasticClient client = null;
        public Class1()
        {
            this.settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("people");
            this.client = new ElasticClient(settings);
        }

        public void Test()
        {
            this.Search1();
        }
        #region Index
        public void Index1()
        {
            var person = new Person
            {
                Id = 1,
                FirstName = "Martijn",
                LastName = "中华人民共和国"
            };

            var indexResponse = client.Index(person);
        }
        public void Index2()
        {
            var createIndexResponse = client.CreateIndex("people", c => c
                .Mappings(m => m
                    .Map<Person>(mm => mm
                        .Properties(p => p
                            .Text(t => t
                                .Name(n => n.LastName)
                                .Analyzer("ik_smart")
                            )
                        )
                    )
                )
            );
        }
        #endregion
        #region Search
        public void Search1()
        {
            var searchResponse = client.Search<Person>(s => s
                //.AllTypes() //people/_search
                //.AllIndices() //_all/person/_search
                .From(0)
                .Size(10)
                .Query(q => q
                     .Match(m => m
                        .Field(f => f.LastName).Analyzer("ik_smart")
                        .Query("中华")
                     )
                )
            );

            var people = searchResponse.Documents;
            if (people.Any())
                Console.WriteLine(people.FirstOrDefault().LastName);
        }
        #endregion
        #region Delete
        public void Delete()
        {
            var all = client.Search<Person>(s => s.Query(q => q.MatchAll()));
            foreach (var doc in all.Documents)
            {
                client.Delete<Person>(doc);
            }
        }
        #endregion
        #region Model
        public class Person
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        #endregion
    }
}
