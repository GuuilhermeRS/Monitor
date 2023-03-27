using System.Collections.Concurrent;
using Monitor.Kernel.Monitor;

namespace Monitor.Kernel.Server;

public class Server
{
    public static ConcurrentDictionary<Client, HashSet<string>> Clients = new();
    public static ConcurrentDictionary<DateTime, Dictionary<string, string>> Messages = new();

    public void RegisterClient(Client client, HashSet<string> topics)
    {
        Clients.TryAdd(client, topics);
    }

    public static async Task RegisterMessage(string topic, string message)
    {
        Messages.TryAdd(DateTime.Now, new Dictionary<string, string> { { topic, message } });
    }

    public async Task Send()
    {
        foreach (var msg in Messages)
        {
            if (msg.Value is null) break;
            
            var subscribed = Clients.Where(s => s.Value.Contains(msg.Value.FirstOrDefault().Key)).ToList();
            if (!subscribed.Any()) continue;
            
            subscribed.ForEach(client => 
                client.Key.Callback(msg.Value.FirstOrDefault().Key, msg.Value.FirstOrDefault().Value));

            Messages.Remove(msg.Key, out var returnValue);
        }
    }

    public async Task FetchData(IMonitor monitor)
    {
        monitor.Ativos.ForEach(async s =>
        {
            await monitor.UpdateAtivo(s.Nome);
            var message = s.Evento.OnPriceChange(s.Nome, s.Preco, s.PrecoAnterior);

            if (!string.IsNullOrEmpty(message))
                await RegisterMessage(monitor.Name, message);
        });
    }
}