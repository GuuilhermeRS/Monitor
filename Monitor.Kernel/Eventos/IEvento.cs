namespace Monitor.Kernel.Eventos;

public interface IEvento
{
    public string OnPriceChange(string ticket, float price, float oldPrice);
}