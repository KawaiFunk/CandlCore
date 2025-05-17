namespace Application.Interfaces.Clients.Coinlore;

public interface ICoinloreHttpClientFactory
{
    HttpClient GetCoinloreHttpClient();
}