using System.Text.Json;
using PlatformService.Dtos;
using System.Text;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient client, IConfiguration configuration)
        {
            _httpClient = client;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}",httpContent);
            if(response.IsSuccessStatusCode){
                Console.WriteLine("--> Sync POST to CommandService was Okay!");
            }else{
                System.Console.WriteLine("--> Sync POST to CommandService was NOT Okay!");
            }
        }
    }
}