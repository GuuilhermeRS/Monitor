namespace Monitor.Kernel.Monitor;

public class ResponseBR
{
    public List<Acao> Results { get; set; }
    
    public ResponseBR() { Results = new List<Acao>(); }
}