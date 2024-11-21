using EventosHenrryGA.Entities;
using System.Net.Http.Json;

namespace EventosHenrryGA.Client.Services
{
    public class ClienteService
    {
        private readonly HttpClient http;

        public ClienteService(HttpClient _http)
        {
            this.http = _http;
        }

        public async Task<List<ClienteCLS>> GetClientes()
        {
            return await http.GetFromJsonAsync<List<ClienteCLS>>("api/cliente");
        }
    }
}
