namespace Infrastructure.BackgroundJobs.CoinloreJob;

public interface ICoinloreJob
{
    Task GetCryptoAssetsAsync();
}