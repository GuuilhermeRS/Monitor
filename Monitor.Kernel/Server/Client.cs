using Monitor.Kernel.Monitor;

namespace Monitor.Kernel.Server;

public class Client
{
    public string Name { get; set; }
    public Func<string, string, string> Callback { get; set; }
    public IMonitor Monitor { get; set; }

    public Client(string name, IMonitor monitor, Func<string, string, string> callback)
    {
        Name = name;
        Monitor = monitor;
        Callback = callback;
    }

    public async Task RegisterAtivo(string nome, string evento)
    {
        await Monitor.GetAtivo(nome, evento);
    }
}