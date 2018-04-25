using DAL.Mpool;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;

namespace MessageClient
{
    class Program
    {

        public static HubConnection hubconnection;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            HubClientInit();


            while (true)
            {
                Console.ReadKey();
                string str = Console.ReadLine();
                hubconnection.InvokeAsync("CreateMessage", "csharp","hello", "123");
            }
        }




        private async static void HubClientInit()
        {
            var Uri = new Uri("http://localhost:5000/MessageHub");
            var connection = new HubConnectionBuilder()
                .WithUrl(Uri)
                .WithConsoleLogger(Microsoft.Extensions.Logging.LogLevel.Information)
                .Build();

            connection.On<string>("ServerResponse", message =>
            {
                Console.WriteLine(message);
            });
            connection.On<dynamic>("PoolList", (poolist) =>
             {
                 Console.WriteLine(JsonConvert.SerializeObject(poolist));
             });

            connection.On<dynamic>("PoolInfo", info =>
            {
                Console.WriteLine(JsonConvert.SerializeObject(info));
            });


            await connection.StartAsync();


            await connection.InvokeAsync("CreatePool", "csharp", "public", "public", "updateTime");
            //connection.InvokeAsync("Ping");
            hubconnection = connection;
        }
    }
}
