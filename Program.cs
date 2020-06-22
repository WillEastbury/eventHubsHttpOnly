using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace eventHubsHttpOnly
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Console.WriteLine("Spinning up a connection.");
            var ehHttpClient = new EhHttpClient("https://willstestns.servicebus.windows.net/messages/messages","RootManageSharedAccessKey","");
            
            // Indvidual
            // await SendMeIndividual(ehHttpClient);

            // Batched, single thread
            // await SendMe(ehHttpClient);

            // Batched, 8X WORKERS (multithreaded senders)
            List<Task> tlist = new List<Task>();
            for (int i = 0; i < 8; i++)
            {
                Task tas = Task.Run(async() => {await SendMe(ehHttpClient);});
                tlist.Add(tas);
            };
            // If we get an aggregate exception we'll return from here if any of our workers bomb out.
            Task.WaitAll(tlist.ToArray());

        }

        private static async Task SendMeIndividual(EhHttpClient ehHttpClient)
        {
            while(1==1)
            {
                List<object> lobj = new List<object>();
                await ehHttpClient.SendSingleMessageAsync(new TelemetryData() { Speed= 160, Gear=2, RandomJunkPadding = "alskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygq"});
            }
        }

        private static async Task SendMe(EhHttpClient ehHttpClient){

            while(1==1)
            {
               
                List<object> lobj = new List<object>();
                
                for (int a = 1; a <= 50 ; a++)
                {
                    
                    lobj.Add(new TelemetryData() { Speed= (160 + a), Gear=2, RandomJunkPadding = "1173" });

                };

                await ehHttpClient.SendMessageBatchAsync(lobj.ToArray());
                Console.WriteLine($"{DateTime.Now.ToString()}: Sent batch on thread #{System.Threading.Thread.CurrentThread.ManagedThreadId}");

            }

        }
    }
    public class TelemetryData
    {
        public int Speed { get; set; }
        public int Gear { get; set; }
        public string RandomJunkPadding {get; set;}
    }
}
