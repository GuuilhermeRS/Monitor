namespace Monitor.Kernel.Eventos;

public class PrecoMedio : IEvento
{
    public string OnPriceChange(string ticker, float price, float oldPrice)
    {
        if (price < oldPrice) return $"{ticker} - Preco caiu {DateTime.Now}";
        return price > oldPrice ? $"{ticker} - Preco subiu {DateTime.Now}" : null;
    }
}