using Monitor.Kernel.Eventos;

namespace Monitor.Kernel;

public class Ativo
{
    public string Nome { get; set; }
    public float Preco { get; set; }
    public float PrecoAnterior { get; set; }
    public DateTime Data { get; set; }
    public IEvento Evento { get; set; }

    public Ativo() { }

    public Ativo(string nome, float preco, string evento)
    {
        Nome = nome;
        Preco = preco;
        PrecoAnterior = preco;
        Data = DateTime.Now;
        
        var tipo = Util.Eventos.GetValueOrDefault(evento);
        if (tipo is null) return;
        
        var constructor = tipo.GetConstructor(Type.EmptyTypes);
        Evento =   (IEvento) constructor?.Invoke(Array.Empty<object>());
    }
    
    public override string ToString()
    {
        return $"Ativo: {Nome} - Valor atual: ${Preco}";
    }
}