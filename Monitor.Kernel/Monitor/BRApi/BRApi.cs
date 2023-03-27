using Monitor.Kernel.Eventos;
using Newtonsoft.Json;

namespace Monitor.Kernel.Monitor;

public class BRApi: IMonitor
{
    private string BaseUrl = "https://brapi.dev/api/quote";
    public string Name { get; set; }
    public List<Ativo> Ativos { get; set; }

    public BRApi(string name)
    {
        Name = name;
        Ativos = new List<Ativo>();
    }
    
    public async Task<Ativo> GetAtivo(string nome, string evento)
    {
        var acao = await GetAcao(nome);
        if (acao is null || Ativos.Find(s => s.Nome == nome) is not null) return null;
        
        Ativos.Add(new Ativo(acao.Symbol, acao.RegularMarketPrice, evento));
        
        return Ativos.FirstOrDefault(s => s.Nome == acao.Symbol);
    }

    public async Task UpdateAtivo(string nome)
    {
        var ativo = Ativos.Find(s => s.Nome == nome);
        var acao = await GetAcao(nome);
        var oldPrice = ativo.Preco;

        ativo.PrecoAnterior = ativo.Preco;
        ativo.Preco = acao.RegularMarketPrice + Random.Shared.Next(0, 10);
        ativo.Data = DateTime.Now;
        
        ativo.Evento.OnPriceChange(ativo.Nome, ativo.Preco, oldPrice);
    }

    private async Task<Acao?> GetAcao(string nome)
    {
        var uri = $"https://brapi.dev/api/quote/{nome.ToUpper()}?range=1d&interval=1d";
        var httpClient = new HttpClient();

        try
        {
            var response = await httpClient.GetAsync(uri);
            var resBody = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ResponseBR>(resBody).Results.FirstOrDefault(s => s.Symbol == nome);
            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);   
        }

        return null;
    }
    
}