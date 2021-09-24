using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.DTOs;

namespace PlatformService.SyncDataServices.Http {
    public class HttpCommandDataClient : ICommandDataClient {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public HttpCommandDataClient( HttpClient http, IConfiguration config ){
            _http = http;
            _config = config;
        }
        public async Task SendToCommandService( PlatformReadDto platform ){
            var httpContent = new StringContent(
                JsonSerializer.Serialize( platform ),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _http.PostAsync( $"{_config["CommandService"]}", httpContent );

            if( response.IsSuccessStatusCode ){
                Console.WriteLine( "--> Sync POST to CommandService was successfull" );
            } else {
                Console.WriteLine( "--> Sync POST to CommandService was not successfull" );
            }
        }
    }
}