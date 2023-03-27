using Monitor.Kernel.Monitor;
using Monitor.Kernel.Server;

namespace Monitor;

public static class Program
{
    public static async Task Main()
    {
        var server = new Server();
        
        var func = new Func<string,string,string>((topic, message) =>
        {
            Console.WriteLine($"{topic} - {message}");
            return null;
        });

        var client1 = new Client("client 1", new BRApi("BR API"), func);
        await client1.RegisterAtivo("PETR4", "preco-medio");
        await client1.RegisterAtivo("MGLU3", "preco-medio");
        
        server.RegisterClient(client1, new HashSet<string>() { "BR API" });

        var update = Task.Run(async () =>
        {
            while (true)
            {
                await server.FetchData(client1.Monitor);
                await Task.Delay(1000);
            }
        });
        
        var send = Task.Run(async () =>
        {
            while (true)
            {
                await server.Send();
                await Task.Delay(1000);
                Console.WriteLine();
            }
        });


        Task.WaitAll(update, send);
    }
}