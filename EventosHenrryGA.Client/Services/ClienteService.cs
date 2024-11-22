using EventosHenrryGA.Entities;
using System.Net.Http.Json;
using System.Text.Json;

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

        public async Task<ClienteCLS> GetCliente(int idCliente)
        {
            return await http.GetFromJsonAsync<ClienteCLS>($"api/cliente/{idCliente}");
        }

        public async Task<string> SaveCliente(ClienteCLS objCliente)
        {
            try
            {
                // ver el objeto que se envia en consola
                Console.WriteLine(JsonSerializer.Serialize(objCliente));

                var response = await http.PostAsJsonAsync("api/cliente", objCliente);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return $"Error: {await response.Content.ReadAsStringAsync()}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }

        public async Task<string> DeleteCliente(int idCliente)
        {

            try
            {
                var response = await http.DeleteAsync($"api/cliente/{idCliente}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                else
                {
                    return $"Error: {await response.Content.ReadAsStringAsync()}";
                }
            }

            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }
    }
}
