using Monitor.Kernel.Eventos;

namespace Monitor.Kernel.Monitor;

public interface IMonitor
{
    public string Name { get; set; }
    public List<Ativo> Ativos { get; set; }

    public Task<Ativo> GetAtivo(string nome, string evento);
    public Task UpdateAtivo(string nome);
}