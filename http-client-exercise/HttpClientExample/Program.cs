using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientExample
{

    class Program
    {
        static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            var urls = new string[]
            {
                "https://docs.microsoft.com",
                "https://google.com",
                "https://piworks.net",
                "https://piworks.net/insight/events",
                "https://piworks.net/insight/news",
                "https://piworks.net/products"
            };
            
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                int sum = 0;
                Task<string>[] taskArr = new Task<string>[6];
                for( int i = 0; i < 6; i++ )
                {
                    taskArr[i] = GetContentAsString( urls[i] );
                }
                for (int i = 0; i < 6; i++)
                {
                    taskArr[i].Wait();
                }
                for (int i = 0; i < 6; i++)
                {
                    sum += taskArr[i].Result.Length;
                    Console.WriteLine( urls[i] + ": " + taskArr[i].Result.Length);
                }

                Console.WriteLine("sum of lengths: " + sum );

                stopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;
                Console.WriteLine("Elamsed Tİme: " + ts );
                return;


            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        private static async Task<string> GetContentAsString(string url)
        {
            HttpResponseMessage response = await client.GetAsync( url );
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
    }
}
