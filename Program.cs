using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eventHubsHttpOnly
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Spinning up a connection.");
            var ehHttpClient = new EhHttpClient("https://f1telemetry.servicebus.windows.net/f1messages/messages","RootManageSharedAccessKey","UTqF4AbaJcTUz7QMDTXzhIhKuQmWmKYHtPG7LTamG2E=");
            int counter = 1;

            // Indvidual
            // while(1==1)
            // {
            //     ++counter; 
            //     Console.WriteLine($"Sending {counter}");
                
            //     List<object> lobj = new List<object>();
                
            //     await ehHttpClient.SendSingleMessageAsync(new TelemetryData() { Speed= (160 + counter), Gear=2, RandomJunkPadding = "alskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygq"});
            //     Console.WriteLine($"Sent message {counter}");

            //     await Task.Delay(300);

            // }

            // Batched, single thread
            // await SendMe(counter,  ehHttpClient);

            // Batched, multithreaded senders
            List<Task> tlist = new List<Task>();

            for (int i = 0; i < 4; i++)
            {
                Task tas = Task.Run(async() => {await SendMe(counter,  ehHttpClient);});
                tlist.Add(tas);
            };
            
            // If we get an aggregate exception we'll return from here if any of our workers bomb out.
            Task.WaitAll(tlist.ToArray());

            Console.WriteLine("Waitall seems to have completed? ");

        }
        public static async Task SendMe(int counter, EhHttpClient ehHttpClient){

            while(1==1)
            {
                ++counter; 
                Console.WriteLine($"Sending {counter}");
                
                List<object> lobj = new List<object>();
                
                for (int a = 1; a <= 50 ; a++)
                {
                    
                    //Console.Write(a);
                    lobj.Add(new TelemetryData() { Speed= (160 + a), Gear=2, RandomJunkPadding = "alskdjfhslfjkasdhfqcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygqalskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygqalskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygqalskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygqalskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygqalskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygqalskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygqalskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygqalskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygqalskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygqalskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygqalskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygqalskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygqalskdjfhslfjkasdhfkjlhsdljkfasdhfsjkdlfhsduifysa90d87uy qcn509tq7349uh356423uyp862yh35uo6yh253uipt793q7rwgyasfdp7igyaghvscygq7hlt23pig8yavsfgyp9q54eghioq3e576h247otygq" });


                };

                await ehHttpClient.SendMessageBatchAsync(lobj.ToArray());
                Console.WriteLine($"Sent batch of 1000 messages {counter}");

                await Task.Delay(300);

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
