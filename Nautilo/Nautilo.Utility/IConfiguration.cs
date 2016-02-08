namespace Nautilo.Utility
{
    public interface IConfiguration
    {
        T GetConfig<T>(string configKey);
    }
}
