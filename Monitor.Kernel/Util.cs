using Monitor.Kernel.Eventos;

namespace Monitor.Kernel;

public static class Util
{
    public static readonly Dictionary<string, Type> Eventos = new ()
    {
        { "candlestick", typeof(Candlestick) }, { "fibonacci", typeof(Fibonacci) }, { "preco-medio", typeof(PrecoMedio) }
    };
}