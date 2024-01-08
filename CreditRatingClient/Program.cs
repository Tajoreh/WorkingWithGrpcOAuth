using System;
using System.Threading.Tasks;
using CreditRatingService;
using Grpc.Net.Client;
using System.Runtime.InteropServices;
using Clients;
using Grpc.Core;

namespace CreditRatingClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serverAddress = "http://127.0.0.1:4000";

            //I Added this line, because i need to send request to none secure server:https://learn.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-7.0#call-insecure-grpc-services-with-net-core-client
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress(serverAddress);

            var client = new ClientQueries.ClientQueriesClient(channel);

            var getClientByIdRequest = new GetClientByIdRequest()
            {
                ClientId = 1
            };

            var reply = await client.GetByIdAsync(getClientByIdRequest);

            Console.WriteLine($"name for client with id= {getClientByIdRequest.ClientId} is {reply.Name}!");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
