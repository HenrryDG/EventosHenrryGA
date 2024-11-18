using EventosHenrryGA.Entities;
using System.Net.Http.Json;

namespace EventosHenrryGA.Client.Services
{
    public class ClienteService
    {
        private readonly HttpClient httpClient;

        public ClienteService(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }

        public async Task<List<ClienteCLS>> GetClientes()
        {
            return await httpClient.GetFromJsonAsync<List<ClienteCLS>>("api/cliente");
        }
    }
}
