using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using CoreTweet;
using CoreTweet.Streaming;
using Nest;
using Newtonsoft.Json;

namespace elasticsearch_net_avr
{
    class Hashtag
    {
        public List<Int32> indices;
        public String text;
    }

    [ElasticsearchType(Name = "tweets")]
    class AvrTweet
    {
        public String Client { get; set; }
        public List<Hashtag> Hashtags { get; set; }
        public String Message { get; set; }
        public String User { get; set; }

        [Date(Name = "@timestamp")]
        public DateTime Timestamp { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            settings.BasicAuthentication("elastic", "changeme");

            var client = new ElasticClient(settings);

            //IndexTweets(client);
            SearchTweets(client);
        }

        private static void IndexTweets(ElasticClient client)
        {
            Tokens tokens = Tokens.Create("BqJoOCvB0O2wFeD70Wsm7RadN", "ySYZt385xmwLxl3dNnD0vrcgkGZrZLq5HXFqmizqNhRqLMVjrw", "255309054-d1X1lhUNPionZ5xGghkdLgVlRp1RJz19xyXOUPyB", "g6ILV8HRdpkABLaJ4E16kBiDhyznYLhCVpUKR0vOd9zTl");

            var disposable = tokens.Streaming
                .FilterAsObservable(track => "Trump")
                .OfType<StatusMessage>()
                .Select(MapTweet)
                .Buffer(5)
                .SelectMany(tweets =>
                    // Déclaration d'une opération Bulk
                    client.BulkAll(
                        // POCOs à indexer.
                        tweets,

                        // Instructions d'indexation.
                        ctx => ctx
                                // Nom de l'index.
                                .Index("tweets-" + DateTime.Now.ToString("yyyy.MM.dd.HH"))
                                // Type du mapping.
                                .Type("tweets")))
                .Subscribe(
                    onNext: response => Console.WriteLine("Indexed a list of 5 tweet."),
                    onError: error => Console.WriteLine("error {0}", error)
                );

            Thread.Sleep(30 * 1000);

            Console.WriteLine("Press some key to stop");

            Console.ReadKey();

            disposable.Dispose();
        }

        private static AvrTweet MapTweet(StatusMessage statusMessage)
        {
            var tweet = new AvrTweet();

            tweet.User = statusMessage.Status.User.Name;
            tweet.Message = statusMessage.Status.Text;
            tweet.Client = statusMessage.Status.Source;
            tweet.Timestamp = DateTime.Now;

            return tweet;
        }

        public static void SearchTweets(ElasticClient client)
        {
            var response = client.Search<AvrTweet>(search =>
                search.Index("tweets")
                    .From(0)
                    .Size(20)
                    .Sort(s => s.Ascending(p => p.Timestamp))
                    .Query(q =>
                        q.Match(mq => mq.Field(f => f.Message).Query("tremendous")
                    )
            ));

            foreach (var hit in response.Hits)
            {
                Console.WriteLine("Date : {0} Tweet : {1}", hit.Source.Timestamp, hit.Source.Message);
            }
        }
    }
}