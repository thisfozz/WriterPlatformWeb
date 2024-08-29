using Consul;
using System.Text.Json;
using System.Text;

namespace WriterPlatformWeb.Services.Implementations;

public class ConsulService
{
    private readonly IConsulClient _consulClient;

    public ConsulService(IConsulClient consulClient)
    {
        _consulClient = consulClient;
    }

    public async Task<string> GetConnectionString()
    {
        var kvPair = await _consulClient.KV.Get("AppConfig/Connection/Database");

        if (kvPair.Response == null) throw new Exception("Не удалось найти ключ в Consul.");

        var jsonString = Encoding.UTF8.GetString(kvPair.Response.Value);
        var jsonDocument = JsonDocument.Parse(jsonString);
        var connectionString = jsonDocument.RootElement.GetProperty("ConnectionString").GetString();

        return connectionString;
    }
}